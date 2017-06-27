using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;

namespace YellowstonePathology.Business.HL7View
{
    public class VentanaStainOrder
    {
        public VentanaStainOrder()
        {

        }

        public string Send(Business.Test.AccessionOrder accessionOrder, string testOrderId, string slideOrderId)
        {
            //protoc -I d:/protogen --csharp_out d:/protogen/result d:/protogen/ventana.proto --grpc_out d:/protogen/result --plugin=protoc-gen-grpc=grpc_csharp_plugin.exe            

            Channel channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);
            Ventana.VentanaService.VentanaServiceClient ventanaServiceClient = new Ventana.VentanaService.VentanaServiceClient(channel);
            Ventana.OrderRequest orderRequest = new Ventana.OrderRequest();

            Business.Test.PanelOrder panelOrder = accessionOrder.PanelSetOrderCollection.GetPanelOrderByTestOrderId(testOrderId);
            Business.User.SystemUser orderedBy = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(panelOrder.OrderedById);

            orderRequest.Msh = new Ventana.msh();
            orderRequest.Msh.SendingApplication = "YPILIS";
            orderRequest.Msh.SendingFacility = "YPI";
            orderRequest.Msh.ReceivingApplication = "Ventana";
            orderRequest.Msh.ReceivingFacility = "YPI";
            orderRequest.Msh.DateTimeOfMessage = DateTime.Now.ToString("yyyyMMddHHmm");
            orderRequest.Msh.MessageType = "OML^O21";
            orderRequest.Msh.MessageControlId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

            orderRequest.Pid = new Ventana.pid();
            orderRequest.Pid.FirstName = accessionOrder.PFirstName;
            orderRequest.Pid.LastName = accessionOrder.PLastName;

            orderRequest.Pid.MiddleInitial = string.IsNullOrEmpty(accessionOrder.PMiddleInitial) ? string.Empty: accessionOrder.PMiddleInitial;
            orderRequest.Pid.Birthdate = accessionOrder.PBirthdate.Value.ToString("yyyyMMdd");
            orderRequest.Pid.Sex = accessionOrder.PSex;

            orderRequest.Pv1 = new Ventana.pv1();
            orderRequest.Pv1.RequestingPhysicianFirstname = orderedBy.FirstName;
            orderRequest.Pv1.RequestingPhysicianLastname = orderedBy.LastName;
            orderRequest.Pv1.RequestingPhysicianNpi = string.IsNullOrEmpty(orderedBy.NationalProviderId) ? string.Empty: orderedBy.NationalProviderId;

            orderRequest.Sac = new Ventana.sac();
            orderRequest.Sac.RegistrationDateTime = accessionOrder.AccessionTime.Value.ToString("yyyyMMddHHmm");

            Ventana.stain_order stainOrder = new Ventana.stain_order();
            Ventana.orc orc = new Ventana.orc();
            orc.OrderControl = "1";
            orc.PlacerOrderNumber = testOrderId;
            orc.SiteName = "YPI";
            orc.SiteDescription = "Yellowstone Pathology";
            orc.FacilityCode = "YPI";
            orc.FacilityName = "Yellowstone Pathology";
            stainOrder.Orc = orc;

            Business.Test.AliquotOrder aliquotOrder = accessionOrder.SpecimenOrderCollection.GetAliquotOrderByTestOrderId(testOrderId);
            Business.Test.Model.TestOrder testOrder = panelOrder.TestOrderCollection.Get(testOrderId);

            Business.Test.Model.VentanaStainCollection ventanaStainCollection = new Test.Model.VentanaStainCollection();
            Business.Test.Model.VentanaStain ventanaStain = ventanaStainCollection.GetByYPITestId(testOrder.TestId.ToString());

            Business.Slide.Model.SlideOrder slideOrder = aliquotOrder.SlideOrderCollection.GetSlideOrderByTestOrderId(testOrderId);
            Business.Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection.GetSpecimenOrderByAliquotOrderId(aliquotOrder.AliquotOrderId);

            Ventana.obr obr = new Ventana.obr();
            obr.OrderSequenceId = "1";
            obr.PlacerOrderNumber = testOrderId;
            obr.ProtocolNumber = ventanaStain.Id;
            obr.ProtocolName = ventanaStain.Name;
            obr.OrderType = ventanaStain.Type;
            obr.ObservationDateTime = DateTime.Now.ToString("yyyyMMddHHmm");
            obr.SpecimenName = specimenOrder.SpecimenId;
            obr.SpecimenDescription = specimenOrder.Description;
            obr.SurgicalProcedureName = "Surgical Pathology";
            obr.PathologistNpi = orderedBy.NationalProviderId;
            obr.PathologistLastname = orderedBy.LastName;
            obr.PathologistFirstname = orderedBy.FirstName;
            obr.SlideId = slideOrder.SlideOrderId;
            obr.SlideSequence = slideOrder.Label;
            obr.Blockid = aliquotOrder.AliquotOrderId;
            obr.BlockSequence = aliquotOrder.Label;
            obr.SpecimenId = specimenOrder.SpecimenId;
            obr.SpecimenSequence = specimenOrder.SpecimenNumber.ToString();
            stainOrder.Obr = obr;
            orderRequest.StainOrders.Add(stainOrder);

            Ventana.OrderReply orderReply = ventanaServiceClient.sendOrder(orderRequest);
            return orderReply.Message;
        }
    }
}
