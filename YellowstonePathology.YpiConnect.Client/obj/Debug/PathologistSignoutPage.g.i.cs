﻿#pragma checksum "..\..\PathologistSignoutPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6BF602CAEC6C9AD88FECBD5184813D27"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using YellowstonePathology.YpiConnect.Client;


namespace YellowstonePathology.YpiConnect.Client {
    
    
    /// <summary>
    /// PathologistSignoutPage
    /// </summary>
    public partial class PathologistSignoutPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 87 "..\..\PathologistSignoutPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewSearchResults;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/YellowstonePathology.YpiConnect.Client;component/pathologistsignoutpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PathologistSignoutPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 44 "..\..\PathologistSignoutPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkHome_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 48 "..\..\PathologistSignoutPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkDetails_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 52 "..\..\PathologistSignoutPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkViewDocument_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 56 "..\..\PathologistSignoutPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkSendMessage_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 66 "..\..\PathologistSignoutPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkRecentCases_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 70 "..\..\PathologistSignoutPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkPatientNameSearch_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 74 "..\..\PathologistSignoutPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkSsnSearch_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 78 "..\..\PathologistSignoutPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkBirthdateSearch_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ListViewSearchResults = ((System.Windows.Controls.ListView)(target));
            
            #line 87 "..\..\PathologistSignoutPage.xaml"
            this.ListViewSearchResults.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ListViewSearchResults_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

