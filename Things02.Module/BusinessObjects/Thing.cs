using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Things02.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Stuff")]
    public class Thing : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //[Aggregated]
        public virtual List<SubThing> SubThings { get; set; }
        IObjectSpace IObjectSpaceLink.ObjectSpace { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnCreated()
        {

        }

        public void OnLoaded()
        {

        }

        public void OnSaving()
        {

        }
    }
}
