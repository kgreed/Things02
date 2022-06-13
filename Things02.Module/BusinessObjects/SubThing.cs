using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Things02.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class SubThing : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ThingId { get; set; }
        [ForeignKey("ThingId")]

        public virtual Thing Thing { get; set; }

        public void Init(object parent)
        {
            Thing = parent as Thing;
        }
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
