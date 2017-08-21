using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using AddInSampleLibrary.Communication;
using CommunicationLibrary;
using WpfElementCommunicationSample.Annotations;

namespace WpfElementCommunicationSample.ViewModel
{
    public class SampleViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<AlarmData> _alarmDataCollection;
        private string _statusMessage;

        public SampleViewModel()
        {
            _alarmDataCollection = new ObservableCollection<AlarmData>();
            AlarmCollection = new ReadOnlyObservableCollection<AlarmData>(_alarmDataCollection);
        }

        public ReadOnlyObservableCollection<AlarmData> AlarmCollection { get; }

        internal void Load()
        {
            try
            {
                StatusMessage = null;

                AlarmData[] lastSelectedAlarmList;
                using (var communicationServiceClient = new ServiceClient<IAlarmService>(Constants.CommunicationServiceEndPointName))
                {
                    communicationServiceClient.Open(nameof(IAlarmService));

                    lastSelectedAlarmList = communicationServiceClient.ServiceProxy.GetLastSelectedAlarmList();
                }

                _alarmDataCollection.Clear();
                if (lastSelectedAlarmList != null)
                {
                    foreach (var alarm in lastSelectedAlarmList)
                    {
                        _alarmDataCollection.Add(alarm);
                    }
                }
            }
            catch (RemotingException)
            {
                StatusMessage = "Please ensure that Data Provider Service is started.";
            }
            catch (Exception e)
            {
                StatusMessage = e.Message;
            }
        }


        /// <summary>
        /// Gets or sets a status message
        /// </summary>
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
