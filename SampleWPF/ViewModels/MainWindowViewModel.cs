using Prism.Commands;
using Prism.Mvvm;
using SampleWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWPF.ViewModels
{
    internal class MainWindowViewModel : BindableBase
    {
        public ShapeManager ShapeManager { get; } = new ShapeManager();

        private DelegateCommand? defaultPenCommand;
        private DelegateCommand? rectanglePenCommand;
        private DelegateCommand? ovalPenCommand;


        public DelegateCommand DefaultPenCommand
        {
            get
            {
                if (defaultPenCommand is null)
                {
                    defaultPenCommand = new DelegateCommand(ShapeManager.SetDefaultPen);
                }
                return defaultPenCommand;
            }
        }

        public DelegateCommand RectanglePenCommand
        {
            get
            {
                if (rectanglePenCommand is null)
                {
                    rectanglePenCommand = new DelegateCommand(ShapeManager.SetRectanglePen);
                }
                return rectanglePenCommand;
            }
        }

        public DelegateCommand OvalPenCommand
        {
            get
            {
                if (ovalPenCommand is null)
                {
                    ovalPenCommand = new DelegateCommand(ShapeManager.SetOvalPen);
                }
                return ovalPenCommand;
            }
        }
    }
}
