﻿#pragma checksum "..\..\ProjectPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "286C2D4D95993BBC2CD3FB998466A7294B72D704CC3AD95A5384DB6C3F55057F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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


namespace AbstractApp {
    
    
    /// <summary>
    /// ProjectPage
    /// </summary>
    public partial class ProjectPage : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\ProjectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid PaperGrid;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\ProjectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ScaleTransform PaperScale;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\ProjectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.TranslateTransform PaperTransform;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\ProjectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ProjektNameText;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\ProjectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ErstellungsdatumText;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\ProjectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnFertig;
        
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
            System.Uri resourceLocater = new System.Uri("/AbstractApp;component/projectpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ProjectPage.xaml"
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
            
            #line 6 "..\..\ProjectPage.xaml"
            ((AbstractApp.ProjectPage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 7 "..\..\ProjectPage.xaml"
            ((AbstractApp.ProjectPage)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.PaperGrid_MouseDown);
            
            #line default
            #line hidden
            
            #line 8 "..\..\ProjectPage.xaml"
            ((AbstractApp.ProjectPage)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.PaperGrid_MouseMove);
            
            #line default
            #line hidden
            
            #line 9 "..\..\ProjectPage.xaml"
            ((AbstractApp.ProjectPage)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.PaperGrid_MouseUp);
            
            #line default
            #line hidden
            
            #line 10 "..\..\ProjectPage.xaml"
            ((AbstractApp.ProjectPage)(target)).MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.Window_MouseWheel);
            
            #line default
            #line hidden
            
            #line 11 "..\..\ProjectPage.xaml"
            ((AbstractApp.ProjectPage)(target)).MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseRightButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PaperGrid = ((System.Windows.Controls.Grid)(target));
            
            #line 17 "..\..\ProjectPage.xaml"
            this.PaperGrid.ContextMenuOpening += new System.Windows.Controls.ContextMenuEventHandler(this.PaperGrid_ContextMenuOpening);
            
            #line default
            #line hidden
            return;
            case 3:
            this.PaperScale = ((System.Windows.Media.ScaleTransform)(target));
            return;
            case 4:
            this.PaperTransform = ((System.Windows.Media.TranslateTransform)(target));
            return;
            case 5:
            
            #line 58 "..\..\ProjectPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_EintragHinzufuegen_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 59 "..\..\ProjectPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_EintragBearbeiten_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 60 "..\..\ProjectPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_EintragVerschieben_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 61 "..\..\ProjectPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_EintragLoeschen_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ProjektNameText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.ErstellungsdatumText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            
            #line 73 "..\..\ProjectPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnZurueck_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.BtnFertig = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\ProjectPage.xaml"
            this.BtnFertig.Click += new System.Windows.RoutedEventHandler(this.BtnFertig_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

