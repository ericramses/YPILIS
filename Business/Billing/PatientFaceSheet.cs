using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace YellowstonePathology.Business.Billing
{
    public class PatientFaceSheet
    {
        public PatientFaceSheet()
        {            
            
        }

        public void Save()
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap(850, 1100, 300, 300, PixelFormats.Default);

            Image imgage = new Image();
            imgage.Stretch = Stretch.None;
            imgage.Source = bitmap;

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            int xCol1 = 5;
            int xCol2 = 65;
            int xCol3 = 135;
            int xCol4 = 197;
            int xCol5 = 115;

            int yCol1 = 10;
                   
            int rowHeight = 6;

            Typeface normalTypeface = new Typeface("Verdana");
            double fontSize = 4;

            FormattedText headerText = new FormattedText("YPI Billing Data", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            headerText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(headerText, new Point(xCol5, yCol1));
            yCol1 += rowHeight +3;

            FormattedText accountNoLabel = new FormattedText("AccountNo:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(accountNoLabel, new Point(xCol1, yCol1));

            FormattedText accountNoText = new FormattedText("488847251", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            accountNoText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(accountNoText, new Point(xCol2, yCol1));

            FormattedText medicalRecordNoLabel = new FormattedText("MedicalRecordNo:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(medicalRecordNoLabel, new Point(xCol3, yCol1));

            FormattedText medicalRecordNoText = new FormattedText("V00054789", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            medicalRecordNoText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(medicalRecordNoText, new Point(xCol4, yCol1));          
            yCol1 += rowHeight;

            FormattedText admitDateLabel = new FormattedText("AdmitDate:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(admitDateLabel, new Point(xCol1, yCol1));

            FormattedText admitDateText = new FormattedText("01/08/12", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            admitDateText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(admitDateText, new Point(xCol2, yCol1));            

            FormattedText dischargeDateLabel = new FormattedText("DischargeDate:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(dischargeDateLabel, new Point(xCol3, yCol1));

            FormattedText dischargeDateText = new FormattedText("01/09/12", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            dischargeDateText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(dischargeDateText, new Point(xCol4, yCol1));
            yCol1 += rowHeight;

            FormattedText accountBaseClassLabel = new FormattedText("AccountBaseClass:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(accountBaseClassLabel, new Point(xCol1, yCol1));

            FormattedText accountBaseClassText = new FormattedText("OP", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            accountBaseClassText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(accountBaseClassText, new Point(xCol2, yCol1));
            yCol1 += rowHeight;

            FormattedText patientNameLabel = new FormattedText("Patient Name:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(patientNameLabel, new Point(xCol1, yCol1));

            FormattedText patientNameText = new FormattedText("Sid Harder", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            patientNameText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(patientNameText, new Point(xCol2, yCol1));

            FormattedText patientDOBLabel = new FormattedText("Patient DOB:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(patientDOBLabel, new Point(xCol3, yCol1));

            FormattedText patientDOBText = new FormattedText("02/12/1965", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            patientDOBText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(patientDOBText, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText patientSexabel = new FormattedText("Patient Sex:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(patientSexabel, new Point(xCol1, yCol1));

            FormattedText patientSexText = new FormattedText("M", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            patientSexText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(patientSexText, new Point(xCol2, yCol1));
            
            FormattedText patientAddress1Label = new FormattedText("PatientAddress:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(patientAddress1Label, new Point(xCol3, yCol1));

            FormattedText patientAddress1Text = new FormattedText("45 Street", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            patientAddress1Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(patientAddress1Text, new Point(xCol4, yCol1));
            yCol1 += rowHeight;

            FormattedText patientAddress2Label = new FormattedText("PatientAddress2:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(patientAddress2Label, new Point(xCol1, yCol1));

            FormattedText patientAddress2Text = new FormattedText("78th Street", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            patientAddress2Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(patientAddress2Text, new Point(xCol2, yCol1));            

            FormattedText patientCityLabel = new FormattedText("PatientCity:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(patientCityLabel, new Point(xCol3, yCol1));

            FormattedText patientCityText = new FormattedText("Billings:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            patientCityText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(patientCityText, new Point(xCol4, yCol1));           
            yCol1 += rowHeight;

            FormattedText patientStateLabel = new FormattedText("PatientState:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(patientStateLabel, new Point(xCol1, yCol1));

            FormattedText patientStateText = new FormattedText("MT", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            patientStateText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(patientStateText, new Point(xCol2, yCol1));            

            FormattedText patientZipLabel = new FormattedText("PatientZIP:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(patientZipLabel, new Point(xCol3, yCol1));

            FormattedText patientZIPText = new FormattedText("59104", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            patientZIPText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(patientZIPText, new Point(xCol4, yCol1));
            yCol1 += rowHeight;

            FormattedText patientHomePhoneLabel = new FormattedText("PateintHomePhone:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(patientHomePhoneLabel, new Point(xCol1, yCol1));
          
            FormattedText patientHomePhoneText = new FormattedText("(406) 789 4563", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            patientHomePhoneText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(patientHomePhoneText, new Point(xCol2, yCol1));            

            FormattedText patientSSNLabel = new FormattedText("PateintSSN:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(patientSSNLabel, new Point(xCol3, yCol1));

            FormattedText patientSSNText = new FormattedText("548-12-4789", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            patientSSNText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(patientSSNText, new Point(xCol4, yCol1));           
            yCol1 += rowHeight +3;

            FormattedText guarantorNameLabel = new FormattedText("Guarantor:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(guarantorNameLabel, new Point(xCol1, yCol1));

            FormattedText guarantorNameText = new FormattedText("Jones, Bill R", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            guarantorNameText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(guarantorNameText, new Point(xCol2, yCol1));            

            FormattedText guarantorAddress1Label = new FormattedText("GuarantorAddress1:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(guarantorAddress1Label, new Point(xCol3, yCol1));

            FormattedText guarantorAddress1Text = new FormattedText("20th Street", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            guarantorAddress1Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(guarantorAddress1Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText guarantorAddress2Label = new FormattedText("GuarantorAddress2:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(guarantorAddress2Label, new Point(xCol1, yCol1));

            FormattedText guarantorAddress2Text = new FormattedText("20th Street", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            guarantorAddress2Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(guarantorAddress2Text, new Point(xCol2, yCol1));            

            FormattedText guarantorCityLabel = new FormattedText("GuarantorCity:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(guarantorCityLabel, new Point(xCol3, yCol1));

            FormattedText guarantorCityText = new FormattedText("Billings", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            guarantorCityText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(guarantorCityText, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText guarantorStateLabel = new FormattedText("GuarantorState:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(guarantorStateLabel, new Point(xCol1, yCol1));

            FormattedText guarantorStateText = new FormattedText("MT", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            guarantorStateText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(guarantorStateText, new Point(xCol2, yCol1));            

            FormattedText guarantorZIPLabel = new FormattedText("GuarantorZIP:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(guarantorZIPLabel, new Point(xCol3, yCol1));

            FormattedText guarantorZIPText = new FormattedText("59103", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            guarantorZIPText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(guarantorZIPText, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText guarantorHomePhoneLabel = new FormattedText("GuarantorHomePhone:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(guarantorHomePhoneLabel, new Point(xCol1, yCol1));

            FormattedText guarantorHomePhoneText = new FormattedText("(406) 456 1323", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            guarantorHomePhoneText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(guarantorHomePhoneText, new Point(xCol2, yCol1));            

            FormattedText guarantorSSNLabel = new FormattedText("GuarantorSSN:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(guarantorSSNLabel, new Point(xCol3, yCol1));

            FormattedText guarantorSSNText = new FormattedText("458-25-4789", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            guarantorSSNText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(guarantorSSNText, new Point(xCol4, yCol1));
            yCol1 += rowHeight;

            FormattedText guarantorDOBLabel = new FormattedText("GuarantorDOB:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(guarantorDOBLabel, new Point(xCol1, yCol1));

            FormattedText guarantorDOBText = new FormattedText("02/15/1978", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            guarantorDOBText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(guarantorDOBText, new Point(xCol2, yCol1));            

            FormattedText guarantorEmployerLabel = new FormattedText("GuarantorEmployer:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(guarantorEmployerLabel, new Point(xCol3, yCol1));

            FormattedText guarantorEmployerText = new FormattedText("Self", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            guarantorEmployerText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(guarantorEmployerText, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText guarantorEmployerAddress1Label = new FormattedText("GuarantorEmpoyerAddress1:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(guarantorEmployerAddress1Label, new Point(xCol1, yCol1));

            FormattedText guarantorEmployerAddress1Text = new FormattedText("PO Box 32", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            guarantorEmployerAddress1Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(guarantorEmployerAddress1Text, new Point(xCol2, yCol1));            

            FormattedText guarantorEmployerAddress2Label = new FormattedText("GuarantorEmpoyerAddress2:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(guarantorEmployerAddress2Label, new Point(xCol3, yCol1));

            FormattedText guarantorEmployerAddress2Text = new FormattedText("7th Street", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            guarantorEmployerAddress2Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(guarantorEmployerAddress2Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText guarantorEmployerCityLabel = new FormattedText("GuarantorEmpoyerCity:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(guarantorEmployerCityLabel, new Point(xCol1, yCol1));

            FormattedText guarantorEmployerCityText = new FormattedText("Billings", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            guarantorEmployerCityText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(guarantorEmployerCityText, new Point(xCol2, yCol1));            

            FormattedText guarantorEmployerStateLabel = new FormattedText("GuarantorEmpoyerST:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(guarantorEmployerStateLabel, new Point(xCol3, yCol1));

            FormattedText guarantorEmployerStateText = new FormattedText("MT", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            guarantorEmployerStateText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(guarantorEmployerStateText, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText guarantorEmployerZipLabel = new FormattedText("GuarantorEmpoyerZip:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(guarantorEmployerZipLabel, new Point(xCol1, yCol1));

            FormattedText guarantorEmployerZipText = new FormattedText("59105", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            guarantorEmployerZipText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(guarantorEmployerZipText, new Point(xCol2, yCol1));            

            FormattedText guarantorEmployerPhoneLabel = new FormattedText("GuarantorEmpoyerPhone:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(guarantorEmployerPhoneLabel, new Point(xCol3, yCol1));

            FormattedText guarantorEmployerPhoneText = new FormattedText("406-456-4789", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            guarantorEmployerPhoneText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(guarantorEmployerZipText, new Point(xCol4, yCol1));            
            yCol1 += rowHeight +3;

            FormattedText attendingProviderLabel = new FormattedText("AttendingProvider:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(attendingProviderLabel, new Point(xCol1, yCol1));

            FormattedText attendingProviderText = new FormattedText("Boucher", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            attendingProviderText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(attendingProviderText, new Point(xCol2, yCol1));            

            FormattedText NPILabel = new FormattedText("NPI:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(NPILabel, new Point(xCol3, yCol1));

            FormattedText NPIText = new FormattedText("1234567890", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            NPIText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(NPIText, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText diagnosisCode1Label = new FormattedText("DiagnosisCode1:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(diagnosisCode1Label, new Point(xCol1, yCol1));

            FormattedText diagnosisCode1Text = new FormattedText("1", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            diagnosisCode1Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(diagnosisCode1Text, new Point(xCol2, yCol1));            

            FormattedText diagnosisCode2Label = new FormattedText("DiagnosisCode2:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(diagnosisCode2Label, new Point(xCol3, yCol1));

            FormattedText diagnosisCode2Text = new FormattedText("2", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            diagnosisCode2Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(diagnosisCode2Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText diagnosisCode3Label = new FormattedText("DiagnosisCode3:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(diagnosisCode3Label, new Point(xCol1, yCol1));

            FormattedText diagnosisCode3Text = new FormattedText("3", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            diagnosisCode3Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(diagnosisCode3Text, new Point(xCol2, yCol1));            

            FormattedText diagnosisCode4Label = new FormattedText("DiagnosisCode4:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(diagnosisCode4Label, new Point(xCol3, yCol1));

            FormattedText diagnosisCode4Text = new FormattedText("4", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            diagnosisCode4Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(diagnosisCode4Text, new Point(xCol4, yCol1));
            yCol1 += rowHeight;

            FormattedText diagnosisCode5Label = new FormattedText("DiagnosisCode5:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(diagnosisCode5Label, new Point(xCol1, yCol1));

            FormattedText diagnosisCode5Text = new FormattedText("5", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            diagnosisCode5Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(diagnosisCode5Text, new Point(xCol2, yCol1));            

            FormattedText diagnosisCode6Label = new FormattedText("DiagnosisCode6:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(diagnosisCode6Label, new Point(xCol3, yCol1));

            FormattedText diagnosisCode6Text = new FormattedText("6", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            diagnosisCode6Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(diagnosisCode6Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText diagnosisCode7Label = new FormattedText("DiagnosisCode7:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(diagnosisCode7Label, new Point(xCol1, yCol1));

            FormattedText diagnosisCode7Text = new FormattedText("7", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            diagnosisCode7Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(diagnosisCode7Text, new Point(xCol2, yCol1));            

            FormattedText diagnosisCode8Label = new FormattedText("DiagnosisCode8:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(diagnosisCode8Label, new Point(xCol3, yCol1));

            FormattedText diagnosisCode8Text = new FormattedText("8", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            diagnosisCode8Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(diagnosisCode8Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText diagnosisCode9Label = new FormattedText("DiagnosisCode9:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(diagnosisCode9Label, new Point(xCol1, yCol1));

            FormattedText diagnosisCode9Text = new FormattedText("9", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            diagnosisCode9Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(diagnosisCode9Text, new Point(xCol2, yCol1));            

            FormattedText diagnosisCode10Label = new FormattedText("DiagnosisCode10:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(diagnosisCode10Label, new Point(xCol3, yCol1));

            FormattedText diagnosisCode10Text = new FormattedText("10", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            diagnosisCode10Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(diagnosisCode10Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight +3;

            FormattedText insurancePlan1Label = new FormattedText("InsurancePlan1:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(insurancePlan1Label, new Point(xCol1, yCol1));

            FormattedText insurancePlan1Text = new FormattedText("BlueCross", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            insurancePlan1Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(insurancePlan1Text, new Point(xCol2, yCol1));            

            FormattedText policyNoPlan1Label = new FormattedText("PolicyNoPlan1:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(policyNoPlan1Label, new Point(xCol3, yCol1));

            FormattedText policyNoPlan1Text = new FormattedText("NoPlan1", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            policyNoPlan1Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(policyNoPlan1Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText groupNoPlan1Label = new FormattedText("GroupNoPlan1:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(groupNoPlan1Label, new Point(xCol1, yCol1));

            FormattedText groupNoPlan1Text = new FormattedText("Group No Plan1", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            groupNoPlan1Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(groupNoPlan1Text, new Point(xCol2, yCol1));            

            FormattedText benefitPlan1Label = new FormattedText("BenefitPlan1:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan1Label, new Point(xCol3, yCol1));

            FormattedText benefitPlan1Text = new FormattedText("BenefitPlan1", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan1Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan1Text, new Point(xCol4, yCol1));           
            yCol1 += rowHeight;

            FormattedText benefitPlan1Address1Label = new FormattedText("BenefitPlan1Address1:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan1Address1Label, new Point(xCol1, yCol1));

            FormattedText benefitPlan1Address1Text = new FormattedText("", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan1Address1Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan1Address1Text, new Point(xCol2, yCol1));            

            FormattedText benefitPlan1Address2Label = new FormattedText("BenefitPlan1Address2:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan1Address2Label, new Point(xCol3, yCol1));

            FormattedText benefitPlan1Address2Text = new FormattedText("NoPlan", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan1Address2Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan1Address2Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText benefitPlan1CityLabel = new FormattedText("BenefitPlan1City:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan1CityLabel, new Point(xCol1, yCol1));

            FormattedText benefitPlan1City1Text = new FormattedText("Harden", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan1City1Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan1City1Text, new Point(xCol2, yCol1));            

            FormattedText benefitPlan1StateLabel = new FormattedText("BenefitPlan1State:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan1StateLabel, new Point(xCol3, yCol1));

            FormattedText benefitPlan1StateText = new FormattedText("MT", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan1StateText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan1StateText, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText benefitPlan1ZIPLabel = new FormattedText("BenefitPlan1ZIP:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan1ZIPLabel, new Point(xCol1, yCol1));

            FormattedText benefitPlan1ZIPText = new FormattedText("59478", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan1ZIPText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan1ZIPText, new Point(xCol2, yCol1));            

            FormattedText benefitPlan1PhoneLabel = new FormattedText("BenefitPlan1Phone:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan1PhoneLabel, new Point(xCol3, yCol1));

            FormattedText benefitPlan1PhoneText = new FormattedText("406 456 7894", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan1PhoneText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan1Address2Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText subscriber1NameLabel = new FormattedText("Subscriber1Name:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(subscriber1NameLabel, new Point(xCol1, yCol1));

            FormattedText subscriber1NameText = new FormattedText("Walters, John R", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            subscriber1NameText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(subscriber1NameText, new Point(xCol2, yCol1));            

            FormattedText subscriber1DOBLabel = new FormattedText("Subscriber1DOB:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(subscriber1DOBLabel, new Point(xCol3, yCol1));

            FormattedText subscriber1DOBText = new FormattedText("02/15/78", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            subscriber1DOBText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(subscriber1DOBText, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText subscriber1SexLabel = new FormattedText("Subscriber1Sex:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(subscriber1SexLabel, new Point(xCol1, yCol1));

            FormattedText subscriber1SexText = new FormattedText("M", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            subscriber1SexText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(subscriber1SexText, new Point(xCol2, yCol1));            

            FormattedText patientReltionToSubscriber1Label = new FormattedText("PatientRelationToSubscriber1:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(patientReltionToSubscriber1Label, new Point(xCol3, yCol1));

            FormattedText patientRelationToSubscriber1Text = new FormattedText("Father", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            patientRelationToSubscriber1Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(patientRelationToSubscriber1Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight +3;

            FormattedText insurancePlan2Label = new FormattedText("InsurancePlan2:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(insurancePlan2Label, new Point(xCol1, yCol1));

            FormattedText insurancePlan2Text = new FormattedText("", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            insurancePlan2Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(insurancePlan2Text, new Point(xCol2, yCol1));

            FormattedText policyNoPlan2Label = new FormattedText("PolicyNoPlan2:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(policyNoPlan2Label, new Point(xCol3, yCol1));

            FormattedText policyNoPlan2Text = new FormattedText("NoPlan2", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            policyNoPlan2Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(policyNoPlan2Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText groupNoPlan2Label = new FormattedText("GroupNoPlan2:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(groupNoPlan2Label, new Point(xCol1, yCol1));

            FormattedText groupNoPlan2Text = new FormattedText("Group No Plan2", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            groupNoPlan2Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(groupNoPlan2Text, new Point(xCol2, yCol1));            

            FormattedText benefitPlan2Label = new FormattedText("BenefitPlan2:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan2Label, new Point(xCol3, yCol1));

            FormattedText benefitPlan2Text = new FormattedText("BenefitPlan2", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan2Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan2Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText benefitPlan2Address1Label = new FormattedText("BenefitPlan2Address1:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan2Address1Label, new Point(xCol1, yCol1));

            FormattedText benefitPlan2Address1Text = new FormattedText("", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan2Address1Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan2Address1Text, new Point(xCol2, yCol1));            

            FormattedText benefitPlan2Address2Label = new FormattedText("BenefitPlan2Address2:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan2Address2Label, new Point(xCol3, yCol1));

            FormattedText benefitPlan2Address2Text = new FormattedText("NoPlan", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan2Address2Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan2Address2Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText benefitPlan2CityLabel = new FormattedText("BenefitPlan2City:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan2CityLabel, new Point(xCol1, yCol1));

            FormattedText benefitPlan2City1Text = new FormattedText("Harden", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan2City1Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan2City1Text, new Point(xCol2, yCol1));            

            FormattedText benefitPlan2StateLabel = new FormattedText("BenefitPlan2State:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan2StateLabel, new Point(xCol3, yCol1));

            FormattedText benefitPlan2StateText = new FormattedText("MT", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan2StateText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan2StateText, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText benefitPlan2ZIPLabel = new FormattedText("BenefitPlan2ZIP:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan2ZIPLabel, new Point(xCol1, yCol1));

            FormattedText benefitPlan2ZIPText = new FormattedText("59478", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan2ZIPText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan2ZIPText, new Point(xCol2, yCol1));            

            FormattedText benefitPlan2PhoneLabel = new FormattedText("BenefitPlan2Phone:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan2PhoneLabel, new Point(xCol3, yCol1));

            FormattedText benefitPlan2PhoneText = new FormattedText("406 456 7894", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan2PhoneText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan2Address2Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText subscriber2NameLabel = new FormattedText("Subscriber2Name:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(subscriber2NameLabel, new Point(xCol1, yCol1));

            FormattedText subscriber2NameText = new FormattedText("Walters, John R", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            subscriber2NameText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(subscriber2NameText, new Point(xCol2, yCol1));            

            FormattedText subscriber2DOBLabel = new FormattedText("Subscriber2DOB:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(subscriber2DOBLabel, new Point(xCol3, yCol1));

            FormattedText subscriber2DOBText = new FormattedText("02/15/78", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            subscriber2DOBText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(subscriber2DOBText, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText subscriber2SexLabel = new FormattedText("Subscriber2Sex:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(subscriber2SexLabel, new Point(xCol1, yCol1));

            FormattedText subscriber2SexText = new FormattedText("M", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            subscriber2SexText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(subscriber2SexText, new Point(xCol2, yCol1));            

            FormattedText patientReltionToSubscriber2Label = new FormattedText("PatientRelationToSubscriber2:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(patientReltionToSubscriber2Label, new Point(xCol3, yCol1));

            FormattedText patientRelationToSubscriber2Text = new FormattedText("Father", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            patientRelationToSubscriber2Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(patientRelationToSubscriber2Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight +3;

            FormattedText insurancePlan3Label = new FormattedText("InsurancePlan3:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(insurancePlan3Label, new Point(xCol1, yCol1));

            FormattedText insurancePlan3Text = new FormattedText("", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            insurancePlan3Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(insurancePlan3Text, new Point(xCol2, yCol1));            

            FormattedText policyNoPlan3Label = new FormattedText("PolicyNoPlan3:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(policyNoPlan3Label, new Point(xCol3, yCol1));

            FormattedText policyNoPlan3Text = new FormattedText("NoPlan3", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            policyNoPlan3Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(policyNoPlan3Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText groupNoPlan3Label = new FormattedText("GroupNoPlan3:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(groupNoPlan3Label, new Point(xCol1, yCol1));

            FormattedText groupNoPlan3Text = new FormattedText("Group No Plan3", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            groupNoPlan3Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(groupNoPlan3Text, new Point(xCol2, yCol1));            

            FormattedText benefitPlan3Label = new FormattedText("BenefitPlan3:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan3Label, new Point(xCol3, yCol1));

            FormattedText benefitPlan3Text = new FormattedText("BenefitPlan3", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan3Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan3Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText benefitPlan3Address1Label = new FormattedText("BenefitPlan3Address1:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan3Address1Label, new Point(xCol1, yCol1));

            FormattedText benefitPlan3Address1Text = new FormattedText("  ", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan3Address1Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan3Address1Text, new Point(xCol2, yCol1));            

            FormattedText benefitPlan3Address2Label = new FormattedText("BenefitPlan3Address2:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan3Address2Label, new Point(xCol3, yCol1));

            FormattedText benefitPlan3Address2Text = new FormattedText("NoPlan", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan3Address2Text.SetFontWeight(FontWeights.Bold); 
            drawingContext.DrawText(benefitPlan3Address2Text, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText benefitPlan3CityLabel = new FormattedText("BenefitPlan3City:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan3CityLabel, new Point(xCol1, yCol1));

            FormattedText benefitPlan3City1Text = new FormattedText("Harden", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan3City1Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan3City1Text, new Point(xCol2, yCol1));            

            FormattedText benefitPlan3StateLabel = new FormattedText("BenefitPlan3State:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan3StateLabel, new Point(xCol3, yCol1));

            FormattedText benefitPlan3StateText = new FormattedText("MT", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan3StateText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan3StateText, new Point(xCol4, yCol1));            
            yCol1 += rowHeight;

            FormattedText benefitPlan3ZIPLabel = new FormattedText("BenefitPlan3ZIP:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan3ZIPLabel, new Point(xCol1, yCol1));

            FormattedText benefitPlan3ZIPText = new FormattedText("59478", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan3ZIPText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan3ZIPText, new Point(xCol2, yCol1));            

            FormattedText benefitPlan3PhoneLabel = new FormattedText("BenefitPlan3Phone:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(benefitPlan3PhoneLabel, new Point(xCol3, yCol1));

            FormattedText benefitPlan3PhoneText = new FormattedText("406 456 7894", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            benefitPlan3PhoneText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(benefitPlan3Address2Text, new Point(xCol4, yCol1));           
            yCol1 += rowHeight;

            FormattedText subscriber3NameLabel = new FormattedText("Subscriber3Name:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(subscriber3NameLabel, new Point(xCol1, yCol1));

            FormattedText subscriber3NameText = new FormattedText("Walters, John R", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            subscriber3NameText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(subscriber3NameText, new Point(xCol2, yCol1));            

            FormattedText subscriber3DOBLabel = new FormattedText("Subscriber3DOB:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(subscriber3DOBLabel, new Point(xCol3, yCol1));

            FormattedText subscriber3DOBText = new FormattedText("02/15/78", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            subscriber3DOBText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(subscriber3DOBText, new Point(xCol4, yCol1));           
            yCol1 += rowHeight;

            FormattedText subscriber3SexLabel = new FormattedText("Subscriber3Sex:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(subscriber3SexLabel, new Point(xCol1, yCol1));

            FormattedText subscriber3SexText = new FormattedText("M", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            subscriber3SexText.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(subscriber3SexText, new Point(xCol2, yCol1));            

            FormattedText patientReltionToSubscriber3Label = new FormattedText("PatientRelationToSubscriber3:", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            drawingContext.DrawText(patientReltionToSubscriber3Label, new Point(xCol3, yCol1));

            FormattedText patientRelationToSubscriber3Text = new FormattedText("Father", System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, normalTypeface, fontSize, Brushes.Black);
            patientRelationToSubscriber3Text.SetFontWeight(FontWeights.Bold);
            drawingContext.DrawText(patientRelationToSubscriber3Text, new Point(xCol4, yCol1));           
            yCol1 += rowHeight;


            drawingContext.Close();
            bitmap.Render(drawingVisual);

            FileStream fileStream = new System.IO.FileStream(@"C:\Testing\TestBitmap.tif", FileMode.Create);
            TiffBitmapEncoder encoder = new TiffBitmapEncoder();
            encoder.Compression = TiffCompressOption.Lzw;
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(fileStream);
            fileStream.Close();         
        }
    }
}
