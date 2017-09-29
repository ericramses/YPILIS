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

        public bool CanBuild(Business.Test.AccessionOrder accessionOrder, string testOrderId, string slideOrderId)
        {
            bool result = false;

            Business.Test.PanelOrder panelOrder = accessionOrder.PanelSetOrderCollection.GetPanelOrderByTestOrderId(testOrderId);
            Business.User.SystemUser orderedBy = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(panelOrder.OrderedById);

            Business.Test.AliquotOrder aliquotOrder = accessionOrder.SpecimenOrderCollection.GetAliquotOrderByTestOrderId(testOrderId);
            Business.Test.Model.TestOrder testOrder = panelOrder.TestOrderCollection.Get(testOrderId);

            Business.Slide.Model.SlideOrder slideOrder = aliquotOrder.SlideOrderCollection.GetSlideOrderByTestOrderId(testOrderId);
            Business.Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection.GetSpecimenOrderByAliquotOrderId(aliquotOrder.AliquotOrderId);

            Business.Surgical.VentanaBenchMarkCollection ventanaBenchMarkCollection = Business.Gateway.AccessionOrderGateway.GetVentanaBenchMarkCollection();
            Business.Surgical.VentanaBenchMark ventanaBenchMark = ventanaBenchMarkCollection.GetByYPITestId(testOrder.TestId.ToString(), slideOrder.UseWetProtocol);

            if (ventanaBenchMark != null) result = true;
            return result;
        }

        public string Build(Business.Test.AccessionOrder accessionOrder, string testOrderId, string slideOrderId)
        {
            //protoc -I d:/protogen --csharp_out d:/protogen/result d:/protogen/ventana.proto --grpc_out d:/protogen/result --plugin=protoc-gen-grpc=grpc_csharp_plugin.exe            

            Channel channel = new Channel("10.1.2.70:32222", ChannelCredentials.Insecure);
            Ventana.VentanaService.VentanaServiceClient ventanaServiceClient = new Ventana.VentanaService.VentanaServiceClient(channel);
            Ventana.OrderRequest orderRequest = new Ventana.OrderRequest();

            Business.Test.PanelOrder panelOrder = accessionOrder.PanelSetOrderCollection.GetPanelOrderByTestOrderId(testOrderId);
            Business.User.SystemUser orderedBy = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(panelOrder.OrderedById);

            Business.Test.AliquotOrder aliquotOrder = accessionOrder.SpecimenOrderCollection.GetAliquotOrderByTestOrderId(testOrderId);
            Business.Test.Model.TestOrder testOrder = panelOrder.TestOrderCollection.Get(testOrderId);            

            Business.Slide.Model.SlideOrder slideOrder = aliquotOrder.SlideOrderCollection.GetSlideOrderByTestOrderId(testOrderId);
            Business.Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection.GetSpecimenOrderByAliquotOrderId(aliquotOrder.AliquotOrderId);

            Business.Surgical.VentanaBenchMarkCollection ventanaBenchMarkCollection = Business.Gateway.AccessionOrderGateway.GetVentanaBenchMarkCollection();
            Business.Surgical.VentanaBenchMark ventanaBenchMark = ventanaBenchMarkCollection.GetByYPITestId(testOrder.TestId.ToString(), slideOrder.UseWetProtocol);

            orderRequest.Msh = new Ventana.msh();
            orderRequest.Msh.SendingApplication = "YPILIS";
            orderRequest.Msh.SendingFacility = "YPI";
            orderRequest.Msh.ReceivingApplication = "Ventana";
            orderRequest.Msh.ReceivingFacility = "YPI";
            orderRequest.Msh.DateTimeOfMessage = DateTime.Now.ToString("yyyyMMddHHmmss");
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
            orderRequest.Pv1.RequestingPhysicianNpi = string.IsNullOrEmpty(orderedBy.NationalProviderId) ? "NOTAPPLICABLE": orderedBy.NationalProviderId;

            orderRequest.Sac = new Ventana.sac();
            orderRequest.Sac.RegistrationDateTime = accessionOrder.AccessionTime.Value.ToString("yyyyMMddHHmm");

            Ventana.stain_order stainOrder = new Ventana.stain_order();
            Ventana.orc orc = new Ventana.orc();
            orc.OrderControl = "NW";
            orc.PlacerOrderNumber = accessionOrder.MasterAccessionNo;
            orc.SiteName = "YPI";
            orc.SiteDescription = "Yellowstone Pathology";
            orc.FacilityCode = "YPI";
            orc.FacilityName = "Yellowstone Pathology";
            stainOrder.Orc = orc;            

            Ventana.obr obr = new Ventana.obr();
            obr.OrderSequenceId = "1";
            obr.PlacerOrderNumber = accessionOrder.MasterAccessionNo;
            obr.ProtocolNumber = ventanaBenchMark.BarcodeNumber.ToString();
            obr.ProtocolName = ventanaBenchMark.StainName;
            obr.OrderType = "STAIN";
            obr.ObservationDateTime = DateTime.Now.ToString("yyyyMMddHHmm");

            if(string.IsNullOrEmpty(specimenOrder.SpecimenId) == false)
            {
                obr.SpecimenName = specimenOrder.SpecimenId;
            }
            else
            {
                obr.SpecimenName = "NONE";
            }
            
            obr.SpecimenDescription = specimenOrder.Description;
            obr.SurgicalProcedureName = "Surgical Pathology";
            obr.PathologistNpi = string.IsNullOrEmpty(orderedBy.NationalProviderId) ? "NOTAPPLICABLE" : orderedBy.NationalProviderId;
            obr.PathologistLastname = orderedBy.LastName;
            obr.PathologistFirstname = orderedBy.FirstName;
            obr.SlideId = "HSLD" + slideOrder.SlideOrderId;
            obr.SlideSequence = Business.Specimen.Model.Slide.GetSlideNumber(slideOrder.Label);
            obr.Blockid = aliquotOrder.AliquotOrderId;
            obr.BlockSequence = Business.Specimen.Model.Block.GetBlockLetter(aliquotOrder.Label);            
            obr.SpecimenId = specimenOrder.SpecimenOrderId;
            obr.SpecimenSequence = specimenOrder.SpecimenNumber.ToString();
            stainOrder.Obr = obr;
            orderRequest.StainOrders.Add(stainOrder);

            Ventana.OrderReply orderReply = ventanaServiceClient.buildOrder(orderRequest);
            return orderReply.Message;
        }
    }
}
