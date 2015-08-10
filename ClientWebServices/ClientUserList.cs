using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace ClientWebServices
{
    public class ClientUserList : List<ClientUser>
    {
        DataContext.YpiData m_DataContext;
        Repository.SearchRepository m_Repository;

        public ClientUserList()
        {
            this.m_DataContext = new ClientWebServices.DataContext.YpiData();
            this.m_Repository = new ClientWebServices.Repository.SearchRepository(this.m_DataContext);                      
        }

        public void FromCode()
        {
            ClientUser svhSurgery = new ClientUser();
            svhSurgery.UserName = "YpiiLab\\SVHSurgery"; //12surgery34
            svhSurgery.DisplayName = "SVH Surgery";
            svhSurgery.ReportFinderPageIsAvailable = true;
            svhSurgery.SynchronizationPageIsAvailable = true;
            svhSurgery.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            svhSurgery.DownloadFileType = "xps";
            svhSurgery.InitialPage = "OrderBrowser";

            this.AddClient(svhSurgery, 760);
            this.AddPhysician(svhSurgery, 646);
            this.AddPhysician(svhSurgery, 273);
            this.AddPhysician(svhSurgery, 604);
            this.AddPhysician(svhSurgery, 84);

            this.Add(svhSurgery);

            ClientUser svhUser = new ClientUser();
            svhUser.UserName = "YpiiLab\\SVHUSER"; //lab5lab
            svhUser.DisplayName = "SVH User";

            this.AddClient(svhUser, 558);
            this.AddClient(svhUser, 57);
            this.AddClient(svhUser, 123);
            this.AddClient(svhUser, 33);
            this.AddClient(svhUser, 230);
            this.AddClient(svhUser, 242);
            this.AddClient(svhUser, 1124);
            this.AddClient(svhUser, 622);
            this.AddClient(svhUser, 505);
            this.AddClient(svhUser, 1151);
            this.AddClient(svhUser, 250);
            this.AddClient(svhUser, 313);
            this.AddClient(svhUser, 759);
            this.AddClient(svhUser, 758);
            this.AddClient(svhUser, 126);
            this.AddClient(svhUser, 820);
            this.AddClient(svhUser, 979);
            this.AddClient(svhUser, 873);
            this.AddClient(svhUser, 760);
            this.AddClient(svhUser, 845);
            this.AddClient(svhUser, 253);
            this.AddClient(svhUser, 1025);
            this.AddClient(svhUser, 744);
            this.AddClient(svhUser, 1058);
            this.AddClient(svhUser, 57);

            svhUser.SynchronizationPageIsAvailable = false;
            svhUser.ReportFinderPageIsAvailable = true;
            svhUser.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            svhUser.InitialPage = "ReportBrowser";
            svhUser.DownloadFileType = "XPS";
            this.Add(svhUser);

            ClientUser marymargret = new ClientUser();
            marymargret.UserName = "YpiiLab\\MARYMARGRET"; //123morgan
            marymargret.DisplayName = "Marymargret Moorehouse";
            this.AddClient(marymargret, 14);
            marymargret.SynchronizationPageIsAvailable = false;
            marymargret.ReportFinderPageIsAvailable = true;
            marymargret.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            marymargret.DownloadFileType = "xps";
            marymargret.InitialPage = "ReportBrowser";
            this.Add(marymargret);

            ClientUser angieDarcy = new ClientUser();
            angieDarcy.UserName = "YpiiLab\\Angie.Darcy"; //angie12345darcy  //Summit Labs //Rocky Mountain Something
            angieDarcy.DisplayName = "Angie Darcy";
            this.AddClient(angieDarcy, 1218);
            this.AddClient(angieDarcy, 1271);
            this.AddClient(angieDarcy, 1225);
            angieDarcy.ReportFinderPageIsAvailable = true;
            angieDarcy.SynchronizationPageIsAvailable = true;
            angieDarcy.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            angieDarcy.DownloadFileType = "xps";
            angieDarcy.InitialPage = "ReportBrowser";
            this.Add(angieDarcy);

            ClientUser lizPompa = new ClientUser();
            lizPompa.UserName = "YPIILAB\\Liz.Crescent"; //123opus
            lizPompa.DisplayName = "Liz Crescent";
            this.AddClient(lizPompa, 1235);
            lizPompa.ReportFinderPageIsAvailable = true;
            lizPompa.SynchronizationPageIsAvailable = true;
            lizPompa.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            lizPompa.DownloadFileType = "xps";
            lizPompa.InitialPage = "ReportBrowser";
            this.Add(lizPompa);

            ClientUser belgradeUC = new ClientUser();
            belgradeUC.UserName = "YPIILAB\\Belgrade.UC"; //harvest1
            belgradeUC.DisplayName = "Belgrade UC";
            this.AddClient(belgradeUC, 743);
            belgradeUC.ReportFinderPageIsAvailable = true;
            belgradeUC.SynchronizationPageIsAvailable = true;
            belgradeUC.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            belgradeUC.DownloadFileType = "xps";
            belgradeUC.InitialPage = "ReportBrowser";
            this.Add(belgradeUC);

            ClientUser westGFC = new ClientUser();
            westGFC.UserName = "YPIILAB\\West.GFC"; //123karen
            westGFC.DisplayName = "West - Great Falls Clinic";
            this.AddClient(westGFC, 1234);
            westGFC.ReportFinderPageIsAvailable = true;
            westGFC.SynchronizationPageIsAvailable = true;
            westGFC.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            westGFC.DownloadFileType = "xps";
            westGFC.InitialPage = "ReportBrowser";
            this.Add(westGFC);

            ClientUser rockyBoy = new ClientUser();
            rockyBoy.UserName = "YPIILAB\\Rocky.Boy"; //123boxelder
            rockyBoy.DisplayName = "Rocky Boy";
            this.AddClient(rockyBoy, 533);
            rockyBoy.ReportFinderPageIsAvailable = true;
            rockyBoy.SynchronizationPageIsAvailable = true;
            rockyBoy.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            rockyBoy.DownloadFileType = "xps";
            rockyBoy.InitialPage = "ReportBrowser";
            this.Add(rockyBoy);

            ClientUser hocnr = new ClientUser();
            hocnr.UserName = "YPIILAB\\HOCNR"; //h1ocnr23
            hocnr.DisplayName = "HOCNR";
            this.AddClient(hocnr, 67);
            hocnr.SynchronizationPageIsAvailable = false;
            hocnr.ReportFinderPageIsAvailable = true;
            hocnr.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            hocnr.DownloadFileType = "xps";
            hocnr.InitialPage = "ReportBrowser";
            this.Add(hocnr);

            ClientUser yellowstoneUrology = new ClientUser();
            yellowstoneUrology.UserName = "YPIILAB\\Yellowstone.Urology"; //503urology
            yellowstoneUrology.DisplayName = "Yellowstone Urology";
            this.AddClient(yellowstoneUrology, 184);
            yellowstoneUrology.SynchronizationPageIsAvailable = false;
            yellowstoneUrology.ReportFinderPageIsAvailable = true;
            yellowstoneUrology.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            yellowstoneUrology.DownloadFileType = "xps";
            yellowstoneUrology.InitialPage = "ReportBrowser";
            this.Add(yellowstoneUrology);

            ClientUser billingsOBGYN = new ClientUser();
            billingsOBGYN.UserName = "YPIILAB\\Billings.OBGYN"; //billings55obgyn
            billingsOBGYN.DisplayName = "Billings OBGYN";
            this.AddClient(billingsOBGYN, 54);
            billingsOBGYN.SynchronizationPageIsAvailable = false;
            billingsOBGYN.ReportFinderPageIsAvailable = true;
            billingsOBGYN.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            billingsOBGYN.DownloadFileType = "xps";
            billingsOBGYN.InitialPage = "ReportBrowser";
            this.Add(billingsOBGYN);

            ClientUser libertyCounty = new ClientUser();
            libertyCounty.UserName = "YPIILAB\\Liberty.County"; //123libertycty
            libertyCounty.DisplayName = "Liberty Medical Center - Hospital Laboratory";
            this.AddClient(libertyCounty, 551);            
            libertyCounty.SynchronizationPageIsAvailable = false;
            libertyCounty.ReportFinderPageIsAvailable = true;
            libertyCounty.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            libertyCounty.DownloadFileType = "xps";
            libertyCounty.InitialPage = "ReportBrowser";
            this.Add(libertyCounty);

            ClientUser libertyClinic = new ClientUser();
            libertyClinic.UserName = "YPIILAB\\Liberty.Clinic"; //liberty88clinic
            libertyClinic.DisplayName = "Liberty Medical Center - Clinic";
            this.AddClient(libertyClinic, 582);
            libertyClinic.SynchronizationPageIsAvailable = false;
            libertyClinic.ReportFinderPageIsAvailable = true;
            libertyClinic.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            libertyClinic.DownloadFileType = "xps";
            libertyClinic.InitialPage = "ReportBrowser";
            this.Add(libertyClinic);

            ClientUser gregoryMcCue = new ClientUser();
            gregoryMcCue.UserName = "YPIILAB\\Gregory.McCue"; //789gmccue
            gregoryMcCue.DisplayName = "West Park Hospital - Gregory McCue";
            this.AddClient(gregoryMcCue, 553);
            this.AddPhysician(gregoryMcCue, 2046);
            gregoryMcCue.SynchronizationPageIsAvailable = false;
            gregoryMcCue.ReportFinderPageIsAvailable = true;
            gregoryMcCue.ClientSearchMode = ClientSearchModeEnum.UsePhysicianList;
            gregoryMcCue.DownloadFileType = "xps";
            gregoryMcCue.InitialPage = "ReportBrowser";
            this.Add(gregoryMcCue);

            ClientUser douglasMorton = new ClientUser();
            douglasMorton.UserName = "YPIILAB\\Douglas.Morton"; //789dmorton
            douglasMorton.DisplayName = "West Park Hospital - Douglas Morton";
            this.AddClient(douglasMorton, 553);
            this.AddPhysician(douglasMorton, 562);
            douglasMorton.SynchronizationPageIsAvailable = false;
            douglasMorton.ReportFinderPageIsAvailable = true;
            douglasMorton.ClientSearchMode = ClientSearchModeEnum.UsePhysicianList;
            douglasMorton.DownloadFileType = "xps";
            douglasMorton.InitialPage = "ReportBrowser";
            this.Add(douglasMorton);

            ClientUser riverCity = new ClientUser();
            riverCity.UserName = "YPIILAB\\River.City"; //familyhealth567
            riverCity.DisplayName = "River City Family Health";
            this.AddClient(riverCity, 1256);
            riverCity.SynchronizationPageIsAvailable = false;
            riverCity.ReportFinderPageIsAvailable = true;
            riverCity.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            riverCity.DownloadFileType = "xps";
            riverCity.InitialPage = "ReportBrowser";
            this.Add(riverCity);

            ClientUser pathAssociates = new ClientUser();
            pathAssociates.UserName = "YPIILAB\\Path.Associates"; //idaho666falls
            pathAssociates.DisplayName = "Pathology Associates of Idaho Falls";
            this.AddClient(pathAssociates, 1201);
            pathAssociates.SynchronizationPageIsAvailable = false;
            pathAssociates.ReportFinderPageIsAvailable = true;
            pathAssociates.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            pathAssociates.DownloadFileType = "xps";
            pathAssociates.InitialPage = "ReportBrowser";
            this.Add(pathAssociates);

            ClientUser entAssociates = new ClientUser();
            entAssociates.UserName = "YPIILAB\\ENT.Associates"; //asc157ent
            entAssociates.DisplayName = "Ear Nose and Throat Associates";
            this.AddClient(entAssociates, 34);
            entAssociates.SynchronizationPageIsAvailable = false;
            entAssociates.ReportFinderPageIsAvailable = true;
            entAssociates.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            entAssociates.DownloadFileType = "xps";
            entAssociates.InitialPage = "ReportBrowser";
            this.Add(entAssociates);

            ClientUser fairfield = new ClientUser();
            fairfield.UserName = "YPIILAB\\Fairfield"; //8KdElmYjtNu3EO
            fairfield.DisplayName = "Fairfield Medical Clinic";
            this.AddClient(fairfield, 252);
            fairfield.SynchronizationPageIsAvailable = false;
            fairfield.ReportFinderPageIsAvailable = true;
            fairfield.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            fairfield.DownloadFileType = "xps";
            fairfield.InitialPage = "ReportBrowser";
            this.Add(fairfield);

            ClientUser bigskyOBGYN = new ClientUser();
            bigskyOBGYN.UserName = "YPIILAB\\Bigsky.OBGYN"; //BTKYoGA9Z6L40H
            bigskyOBGYN.DisplayName = "Big Sky OBG/YN";
            this.AddClient(bigskyOBGYN, 25);
            bigskyOBGYN.SynchronizationPageIsAvailable = false;
            bigskyOBGYN.ReportFinderPageIsAvailable = true;
            bigskyOBGYN.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            bigskyOBGYN.DownloadFileType = "xps";
            bigskyOBGYN.InitialPage = "ReportBrowser";
            this.Add(bigskyOBGYN);

            ClientUser surgicalAssociates = new ClientUser();
            surgicalAssociates.UserName = "YPIILAB\\Surgical.Associates"; //123surgical456
            surgicalAssociates.DisplayName = "Surgical Associates";
            this.AddClient(surgicalAssociates, 104);
            surgicalAssociates.SynchronizationPageIsAvailable = false;
            surgicalAssociates.ReportFinderPageIsAvailable = true;
            surgicalAssociates.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            surgicalAssociates.DownloadFileType = "xps";
            surgicalAssociates.InitialPage = "ReportBrowser";
            this.Add(surgicalAssociates);

            ClientUser northernRockiesSurgeryCenter = new ClientUser();
            northernRockiesSurgeryCenter.UserName = "YPIILAB\\NorthernRockies.Surg"; //SjnnH3MRe8US9S
            northernRockiesSurgeryCenter.DisplayName = "Northern Rockies Surgery Center";
            this.AddClient(northernRockiesSurgeryCenter, 225);
            northernRockiesSurgeryCenter.SynchronizationPageIsAvailable = false;
            northernRockiesSurgeryCenter.ReportFinderPageIsAvailable = true;
            northernRockiesSurgeryCenter.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            northernRockiesSurgeryCenter.DownloadFileType = "xps";
            northernRockiesSurgeryCenter.InitialPage = "ReportBrowser";
            this.Add(northernRockiesSurgeryCenter);

            ClientUser elizabethSeton = new ClientUser();
            elizabethSeton.UserName = "YPIILAB\\Elizabeth.Seton"; //prenatalclinic123
            elizabethSeton.DisplayName = "Elizabeth Seton Prenatal Clinic";
            this.AddClient(elizabethSeton, 253);
            elizabethSeton.SynchronizationPageIsAvailable = false;
            elizabethSeton.ReportFinderPageIsAvailable = true;
            elizabethSeton.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            elizabethSeton.DownloadFileType = "xps";
            elizabethSeton.InitialPage = "ReportBrowser";
            this.Add(elizabethSeton);

            ClientUser annetteComes = new ClientUser();
            annetteComes.UserName = "YPIILAB\\Annette.Comes"; //WV97qHTqTKuC6e
            annetteComes.DisplayName = "Annette Comes";
            this.AddClient(annetteComes, 756);
            annetteComes.SynchronizationPageIsAvailable = false;
            annetteComes.ReportFinderPageIsAvailable = true;
            annetteComes.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            annetteComes.DownloadFileType = "xps";
            annetteComes.InitialPage = "ReportBrowser";
            this.Add(annetteComes);

            ClientUser pcWesternMontana = new ClientUser();
            pcWesternMontana.UserName = "YPIILAB\\PC.WesternMontana"; //Z8ejgm7UUjN3Ut
            pcWesternMontana.DisplayName = "Pathology Consultants of Western Montana";
            this.AddClient(pcWesternMontana, 1207);
            this.AddClient(pcWesternMontana, 726);
            this.AddClient(pcWesternMontana, 1174);
            pcWesternMontana.SynchronizationPageIsAvailable = false;
            pcWesternMontana.ReportFinderPageIsAvailable = true;
            pcWesternMontana.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            pcWesternMontana.DownloadFileType = "xps";
            pcWesternMontana.InitialPage = "ReportBrowser";
            this.Add(pcWesternMontana);

            ClientUser hardinClinic = new ClientUser();
            hardinClinic.UserName = "YPIILAB\\Hardin.Clinic"; //clinic789hardin
            hardinClinic.DisplayName = "Hardin Clinic";
            this.AddClient(hardinClinic, 250);
            hardinClinic.SynchronizationPageIsAvailable = false;
            hardinClinic.ReportFinderPageIsAvailable = true;
            hardinClinic.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            hardinClinic.DownloadFileType = "xps";
            hardinClinic.InitialPage = "ReportBrowser";
            this.Add(hardinClinic);

            ClientUser threeRiversClinic = new ClientUser();
            threeRiversClinic.UserName = "YPIILAB\\Three.Rivers"; //niFUSTCy3BBirF
            threeRiversClinic.DisplayName = "Three Rivers Clinic";
            this.AddClient(threeRiversClinic, 1144);
            threeRiversClinic.SynchronizationPageIsAvailable = false;
            threeRiversClinic.ReportFinderPageIsAvailable = true;
            threeRiversClinic.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            threeRiversClinic.DownloadFileType = "xps";
            threeRiversClinic.InitialPage = "ReportBrowser";
            this.Add(threeRiversClinic);

            ClientUser hrhIMOBGYN = new ClientUser();
            hrhIMOBGYN.UserName = "YPIILAB\\HRH.IMOBGYN"; //sndoSL1brGMoqp
            hrhIMOBGYN.DisplayName = "HRH OB/GYN";
            this.AddClient(hrhIMOBGYN, 723);
            this.AddClient(hrhIMOBGYN, 649);
            hrhIMOBGYN.SynchronizationPageIsAvailable = false;
            hrhIMOBGYN.ReportFinderPageIsAvailable = true;
            hrhIMOBGYN.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            hrhIMOBGYN.DownloadFileType = "xps";
            hrhIMOBGYN.InitialPage = "ReportBrowser";
            this.Add(hrhIMOBGYN);

            ClientUser laurelMedCtr = new ClientUser();
            laurelMedCtr.UserName = "YPIILAB\\Laurel.MedicalCenter"; //laurel333medctr
            laurelMedCtr.DisplayName = "Laurel Medical Center";
            this.AddClient(laurelMedCtr, 123);
            laurelMedCtr.SynchronizationPageIsAvailable = false;
            laurelMedCtr.ReportFinderPageIsAvailable = true;
            laurelMedCtr.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            laurelMedCtr.DownloadFileType = "xps";
            laurelMedCtr.InitialPage = "ReportBrowser";
            this.Add(laurelMedCtr);

            ClientUser springCreek = new ClientUser();
            springCreek.UserName = "YPIILAB\\SpringCreek"; //gw1Ln66zhBnLPs
            springCreek.DisplayName = "Spring Creek Medical Center";
            this.AddClient(springCreek, 1177);
            springCreek.SynchronizationPageIsAvailable = false;
            springCreek.ReportFinderPageIsAvailable = true;
            springCreek.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            springCreek.DownloadFileType = "xps";
            springCreek.InitialPage = "ReportBrowser";
            this.Add(springCreek);

            ClientUser internalMedicine = new ClientUser();
            internalMedicine.UserName = "YPIILAB\\Internalmedicine"; //lsRfFOlHSpAwcE
            internalMedicine.DisplayName = "Internal Medicine";
            this.AddClient(internalMedicine, 33);
            internalMedicine.SynchronizationPageIsAvailable = false;
            internalMedicine.ReportFinderPageIsAvailable = true;
            internalMedicine.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            internalMedicine.DownloadFileType = "xps";
            internalMedicine.InitialPage = "ReportBrowser";
            this.Add(internalMedicine);

            ClientUser mercuryStreet = new ClientUser();
            mercuryStreet.UserName = "YPIILAB\\MercuryStreet"; //street654321
            mercuryStreet.DisplayName = "Mercury Street";
            this.AddClient(mercuryStreet, 1258);
            this.AddClient(mercuryStreet, 819);
            this.AddClient(mercuryStreet, 988);
            mercuryStreet.SynchronizationPageIsAvailable = false;
            mercuryStreet.ReportFinderPageIsAvailable = true;
            mercuryStreet.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            mercuryStreet.DownloadFileType = "xps";
            mercuryStreet.InitialPage = "ReportBrowser";
            this.Add(mercuryStreet);

            ClientUser focusMedcare = new ClientUser();
            focusMedcare.UserName = "YPIILAB\\FocusMedCare"; //focus6611
            focusMedcare.DisplayName = "Focus MedCare";
            this.AddClient(focusMedcare, 988);
            focusMedcare.SynchronizationPageIsAvailable = false;
            focusMedcare.ReportFinderPageIsAvailable = true;
            focusMedcare.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            focusMedcare.DownloadFileType = "xps";
            focusMedcare.InitialPage = "ReportBrowser";
            this.Add(focusMedcare);

            ClientUser yellowstoneBreastCenter = new ClientUser();
            yellowstoneBreastCenter.UserName = "YPIILAB\\Yellowstone.Breast"; //kCpdS4OFdDM7S4
            yellowstoneBreastCenter.DisplayName = "Yellowstone Breast Center";
            this.AddClient(yellowstoneBreastCenter, 126);
            yellowstoneBreastCenter.SynchronizationPageIsAvailable = false;
            yellowstoneBreastCenter.ReportFinderPageIsAvailable = true;
            yellowstoneBreastCenter.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            yellowstoneBreastCenter.DownloadFileType = "xps";
            yellowstoneBreastCenter.InitialPage = "ReportBrowser";
            this.Add(yellowstoneBreastCenter);

            ClientUser tomBeeson = new ClientUser();
            tomBeeson.UserName = "YPIILAB\\Tom.Beeson"; //TipasSs87RZ24q
            tomBeeson.DisplayName = "Thomas Beeson";
            this.AddClient(tomBeeson, 1001);
            tomBeeson.SynchronizationPageIsAvailable = false;
            tomBeeson.ReportFinderPageIsAvailable = true;
            tomBeeson.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            tomBeeson.DownloadFileType = "xps";
            tomBeeson.InitialPage = "ReportBrowser";
            this.Add(tomBeeson);

            ClientUser westGrand = new ClientUser();
            westGrand.UserName = "YPIILAB\\West.Grand"; //HD5qqbqusgr3Yz
            westGrand.DisplayName = "West Grand Family Medicine";
            this.AddClient(westGrand, 242);
            westGrand.SynchronizationPageIsAvailable = false;
            westGrand.ReportFinderPageIsAvailable = true;
            westGrand.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            westGrand.DownloadFileType = "xps";
            westGrand.InitialPage = "ReportBrowser";
            this.Add(westGrand);

            ClientUser hotSprings = new ClientUser();
            hotSprings.UserName = "YPIILAB\\Hot.Springs"; //ad3pQueqgIzKp5
            hotSprings.DisplayName = "Hot Springs";
            this.AddClient(hotSprings, 1034);
            hotSprings.SynchronizationPageIsAvailable = false;
            hotSprings.ReportFinderPageIsAvailable = true;
            hotSprings.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            hotSprings.DownloadFileType = "xps";
            hotSprings.InitialPage = "ReportBrowser";
            this.Add(hotSprings);

            ClientUser hotSpringsMR = new ClientUser();
            hotSpringsMR.UserName = "YPIILAB\\Hot.SpringsMR"; //hot543Medical
            hotSpringsMR.DisplayName = "Hot Springs - Medical Records";
            this.AddClient(hotSpringsMR, 1089);
            hotSpringsMR.SynchronizationPageIsAvailable = false;
            hotSpringsMR.ReportFinderPageIsAvailable = true;
            hotSpringsMR.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            hotSpringsMR.DownloadFileType = "xps";
            hotSpringsMR.InitialPage = "ReportBrowser";
            this.Add(hotSpringsMR);

            ClientUser vernonMiller = new ClientUser();
            vernonMiller.UserName = "YPIILAB\\Vernon.Miller"; //123456vernon
            vernonMiller.DisplayName = "Vernon Miller";
            //this.AddPhysician(vernonMiller, 986);
            this.AddClient(vernonMiller, 1051);
            vernonMiller.SynchronizationPageIsAvailable = false;
            vernonMiller.ReportFinderPageIsAvailable = true;
            vernonMiller.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            vernonMiller.DownloadFileType = "xps";
            vernonMiller.InitialPage = "ReportBrowser";
            this.Add(vernonMiller);

            ClientUser mooreMedicalClinic = new ClientUser();
            mooreMedicalClinic.UserName = "YPIILAB\\Moore.Medical"; //4LUNjXxyXsjaCH
            mooreMedicalClinic.DisplayName = "Moore Medical Clinic";
            this.AddClient(mooreMedicalClinic, 1264);
            mooreMedicalClinic.SynchronizationPageIsAvailable = false;
            mooreMedicalClinic.ReportFinderPageIsAvailable = true;
            mooreMedicalClinic.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            mooreMedicalClinic.DownloadFileType = "xps";
            mooreMedicalClinic.InitialPage = "ReportBrowser";
            this.Add(mooreMedicalClinic);

            ClientUser premier = new ClientUser();
            premier.UserName = "YPIILAB\\Premier"; //vCazRayGjTREpH
            premier.DisplayName = "Premier Family Practice";
            this.AddClient(premier, 1263);
            premier.SynchronizationPageIsAvailable = false;
            premier.ReportFinderPageIsAvailable = true;
            premier.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            premier.DownloadFileType = "xps";
            premier.InitialPage = "ReportBrowser";
            this.Add(premier);

            ClientUser plentywood = new ClientUser();
            plentywood.UserName = "YPIILAB\\Plentywood"; //Kg4LHQUXVPesJW
            plentywood.DisplayName = "Plentywood Clinic";
            this.AddClient(plentywood, 721);
            plentywood.SynchronizationPageIsAvailable = false;
            plentywood.ReportFinderPageIsAvailable = true;
            plentywood.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            plentywood.DownloadFileType = "pdf";
            plentywood.InitialPage = "ReportBrowser";
            this.Add(plentywood);

            ClientUser lauraBennett = new ClientUser();
            lauraBennett.UserName = "YPIILAB\\Laura.Bennett"; //yY7Z2e9vueYSZ2
            lauraBennett.DisplayName = "Laura Bennett";
            this.AddClient(lauraBennett, 209);
            lauraBennett.SynchronizationPageIsAvailable = false;
            lauraBennett.ReportFinderPageIsAvailable = true;
            lauraBennett.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            lauraBennett.DownloadFileType = "xps";
            lauraBennett.InitialPage = "ReportBrowser";
            this.Add(lauraBennett);

            ClientUser charlesWelch = new ClientUser();
            charlesWelch.UserName = "YPIILAB\\charles.welch"; //QE2TNYp8VEpumj
            charlesWelch.DisplayName = "Charles Welch";
            this.AddClient(charlesWelch, 1127);
            charlesWelch.SynchronizationPageIsAvailable = false;
            charlesWelch.ReportFinderPageIsAvailable = true;
            charlesWelch.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            charlesWelch.DownloadFileType = "xps";
            charlesWelch.InitialPage = "ReportBrowser";
            this.Add(charlesWelch);

            ClientUser arthritisCenter = new ClientUser();
            arthritisCenter.UserName = "YPIILAB\\arthritis.center"; //4AKUxyrUd26u2x
            arthritisCenter.DisplayName = "Arthritis Center";
            this.AddClient(arthritisCenter, 152);
            arthritisCenter.SynchronizationPageIsAvailable = false;
            arthritisCenter.ReportFinderPageIsAvailable = true;
            arthritisCenter.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            arthritisCenter.DownloadFileType = "xps";
            arthritisCenter.InitialPage = "ReportBrowser";
            this.Add(arthritisCenter);

            ClientUser stPatricks = new ClientUser();
            stPatricks.UserName = "YPIILAB\\stpatricks"; //pNnGRjtbMGwlwr
            stPatricks.DisplayName = "St. Patricks";
            this.AddClient(stPatricks, 1174);
            this.AddClient(stPatricks, 1131);
            this.AddClient(stPatricks, 1114);
            this.AddClient(stPatricks, 983);
            this.AddClient(stPatricks, 1207);
            this.AddClient(stPatricks, 726);
            stPatricks.SynchronizationPageIsAvailable = false;
            stPatricks.ReportFinderPageIsAvailable = true;
            stPatricks.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            stPatricks.DownloadFileType = "xps";
            stPatricks.InitialPage = "ReportBrowser";
            this.Add(stPatricks);

            ClientUser yellowstoneSurgery = new ClientUser();
            yellowstoneSurgery.UserName = "YPIILAB\\yellowstone.surgery"; /// aiCSbYAwNKXT9p
            yellowstoneSurgery.DisplayName = "Yellowstone Surgery Center";
            this.AddClient(yellowstoneSurgery, 660);
            yellowstoneSurgery.SynchronizationPageIsAvailable = false;
            yellowstoneSurgery.ReportFinderPageIsAvailable = true;
            yellowstoneSurgery.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            yellowstoneSurgery.DownloadFileType = "xps";
            yellowstoneSurgery.InitialPage = "ReportBrowser";
            this.Add(yellowstoneSurgery);

            ClientUser davidJohnson = new ClientUser();
            davidJohnson.UserName = "YPIILAB\\David.Johnson"; /// 9999david
            davidJohnson.DisplayName = "David Johnson";
            this.AddPhysician(davidJohnson, 64);
            davidJohnson.SynchronizationPageIsAvailable = false;
            davidJohnson.ReportFinderPageIsAvailable = true;
            davidJohnson.ClientSearchMode = ClientSearchModeEnum.UsePhysicianList;
            davidJohnson.DownloadFileType = "xps";
            davidJohnson.InitialPage = "ReportBrowser";
            this.Add(davidJohnson);

            
            ClientUser northernMontana = new ClientUser();
            northernMontana.UserName = "YPIILAB\\Northern.Montana"; //47MF5YMr
            northernMontana.DisplayName = "Northern Montana Hospital";
            this.AddClient(northernMontana, 587);
            northernMontana.SynchronizationPageIsAvailable = false;
            northernMontana.ReportFinderPageIsAvailable = true;
            northernMontana.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            northernMontana.DownloadFileType = "xps";
            northernMontana.InitialPage = "ReportBrowser";
            this.Add(northernMontana);

            ClientUser williamEvans = new ClientUser();
            williamEvans.UserName = "YPIILAB\\William.Evans"; //55William44
            williamEvans.DisplayName = "William Evans";
            this.AddClient(williamEvans, 1211);
            williamEvans.SynchronizationPageIsAvailable = false;
            williamEvans.ReportFinderPageIsAvailable = true;
            williamEvans.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            williamEvans.DownloadFileType = "xps";
            williamEvans.InitialPage = "ReportBrowser";
            this.Add(williamEvans);

            ClientUser northernWyoming = new ClientUser();
            northernWyoming.UserName = "YPIILAB\\Northern.Wyoming"; //00SurgicalWy
            northernWyoming.DisplayName = "Northern Wyoming Surgical Center";
            this.AddClient(northernWyoming, 1046);
            northernWyoming.SynchronizationPageIsAvailable = false;
            northernWyoming.ReportFinderPageIsAvailable = true;
            northernWyoming.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            northernWyoming.DownloadFileType = "xps";
            northernWyoming.InitialPage = "ReportBrowser";
            this.Add(northernWyoming);

            ClientUser lisaWilliams = new ClientUser();
            lisaWilliams.UserName = "YPIILAB\\Lisa.Williams"; //888lisa
            lisaWilliams.DisplayName = "Lisa Williams";
            this.AddClient(lisaWilliams, 928);
            lisaWilliams.SynchronizationPageIsAvailable = false;
            lisaWilliams.ReportFinderPageIsAvailable = true;
            lisaWilliams.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            lisaWilliams.DownloadFileType = "xps";
            lisaWilliams.InitialPage = "ReportBrowser";
            this.Add(lisaWilliams);

            ClientUser midwayClinic = new ClientUser();
            midwayClinic.UserName = "YPIILAB\\Midway.Clinic"; //midwayBBBclinic
            midwayClinic.DisplayName = "Midway Clinic";
            this.AddClient(midwayClinic, 1037);
            midwayClinic.SynchronizationPageIsAvailable = false;
            midwayClinic.ReportFinderPageIsAvailable = true;
            midwayClinic.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            midwayClinic.DownloadFileType = "xps";
            midwayClinic.InitialPage = "ReportBrowser";
            this.Add(midwayClinic);

            ClientUser powellValley = new ClientUser();
            powellValley.UserName = "YPIILAB\\Powell.Valley"; //pv67mr
            powellValley.DisplayName = "Powell Valley";
            this.AddClient(powellValley, 312);
            powellValley.SynchronizationPageIsAvailable = false;
            powellValley.ReportFinderPageIsAvailable = true;
            powellValley.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            powellValley.DownloadFileType = "xps";
            powellValley.InitialPage = "ReportBrowser";
            this.Add(powellValley);

            ClientUser ronaldLinfesty = new ClientUser();
            ronaldLinfesty.UserName = "YPIILAB\\Ronald.Linfesty"; //ronald234Fes
            ronaldLinfesty.DisplayName = "Dr. Ronald Linfesty";
            this.AddClient(ronaldLinfesty, 560);
            ronaldLinfesty.SynchronizationPageIsAvailable = false;
            ronaldLinfesty.ReportFinderPageIsAvailable = true;
            ronaldLinfesty.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            ronaldLinfesty.DownloadFileType = "xps";
            ronaldLinfesty.InitialPage = "ReportBrowser";
            this.Add(ronaldLinfesty);

            ClientUser wendyGillis = new ClientUser();
            wendyGillis.UserName = "YPIILAB\\Wendy.Gillis"; //1ABCGillis
            wendyGillis.DisplayName = "Wendy Gillis";
            this.AddClient(wendyGillis, 1300);
            wendyGillis.SynchronizationPageIsAvailable = false;
            wendyGillis.ReportFinderPageIsAvailable = true;
            wendyGillis.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            wendyGillis.DownloadFileType = "xps";
            wendyGillis.InitialPage = "ReportBrowser";
            this.Add(wendyGillis);

            ClientUser s006782 = new ClientUser();
            s006782.UserName = "YPIILAB\\S006782"; //S006782
            s006782.DisplayName = "Chelsea Czeczel";
            this.AddClient(s006782, 230);
            s006782.SynchronizationPageIsAvailable = false;
            s006782.ReportFinderPageIsAvailable = true;
            s006782.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            s006782.DownloadFileType = "xps";
            s006782.InitialPage = "ReportBrowser";
            this.Add(s006782);

            ClientUser billingClinic = new ClientUser();
            billingClinic.UserName = "YPIILAB\\Billings.Clinic"; //Blgs765Clinic
            billingClinic.DisplayName = "Billings Clinic";
            this.AddClient(billingClinic, 560);
            billingClinic.SynchronizationPageIsAvailable = false;
            billingClinic.ReportFinderPageIsAvailable = true;
            billingClinic.ClientSearchMode = ClientSearchModeEnum.UseClientList;
            billingClinic.DownloadFileType = "xps";
            billingClinic.InitialPage = "ReportBrowser";
            this.Add(billingClinic);
        }       

        public void AddClient(ClientUser clientUser, int clientId)
        {
            Domain.Client client = this.m_Repository.GetClient(clientId);            
            clientUser.Clients.Add(client);            
        }

        public void AddPhysician(ClientUser clientUser, int physicianId)
        {
            Domain.Physician physician = this.m_Repository.GetPhysician(physicianId);            
            clientUser.Physicians.Add(physician);            
        }

        public ClientUser GetCurrentUser()
        {
            ClientUser result = null;
            ServiceSecurityContext securityContext = ServiceSecurityContext.Current;

            foreach (ClientUser clientUser in this)
            {
                if (clientUser.UserName.ToUpper() == securityContext.PrimaryIdentity.Name.ToUpper())
                {
                    result = clientUser;
                }
            }
            return result;
        }
    }
}
