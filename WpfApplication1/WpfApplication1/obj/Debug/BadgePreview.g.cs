﻿#pragma checksum "..\..\BadgePreview.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8A85676B5B0AB40B852DAC1671371C3BAA48F275"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SchoolSafeID;
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


namespace SchoolSafeID {
    
    
    /// <summary>
    /// BadgePreview
    /// </summary>
    public partial class BadgePreview : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\BadgePreview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_GoBack;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\BadgePreview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtSchoolName;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\BadgePreview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtVisitorName;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\BadgePreview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtVisitorType;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\BadgePreview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtDate;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\BadgePreview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtTime;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\BadgePreview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgVisitorImage;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\BadgePreview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtDestination;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\BadgePreview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnYesPrint;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\BadgePreview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNoMakeChanges;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\BadgePreview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNoBadgeNeeded;
        
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
            System.Uri resourceLocater = new System.Uri("/SchoolSafeID;component/badgepreview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\BadgePreview.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            
            #line 8 "..\..\BadgePreview.xaml"
            ((SchoolSafeID.BadgePreview)(target)).Loaded += new System.Windows.RoutedEventHandler(this.page_Loaded);
            
            #line default
            #line hidden
            
            #line 8 "..\..\BadgePreview.xaml"
            ((SchoolSafeID.BadgePreview)(target)).Unloaded += new System.Windows.RoutedEventHandler(this.Page_Unloaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btn_GoBack = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\BadgePreview.xaml"
            this.btn_GoBack.Click += new System.Windows.RoutedEventHandler(this.btn_GoBack_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtSchoolName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.txtVisitorName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.txtVisitorType = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.txtDate = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.txtTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.imgVisitorImage = ((System.Windows.Controls.Image)(target));
            return;
            case 9:
            this.txtDestination = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.btnYesPrint = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\BadgePreview.xaml"
            this.btnYesPrint.Click += new System.Windows.RoutedEventHandler(this.btnYesPrint_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnNoMakeChanges = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\BadgePreview.xaml"
            this.btnNoMakeChanges.Click += new System.Windows.RoutedEventHandler(this.btnNoMakeChanges_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btnNoBadgeNeeded = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\BadgePreview.xaml"
            this.btnNoBadgeNeeded.Click += new System.Windows.RoutedEventHandler(this.btnNoBadgeNeeded_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

