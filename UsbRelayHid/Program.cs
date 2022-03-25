using System;
using System.Linq;
using UsbRelayNet.RelayLib;

namespace UsbRelayHid
{
    internal class Program
    {
        private static readonly RelaysEnumerator _relaysEnumerator = new RelaysEnumerator();
        private static Relay _selectedRelay=null;


        private static void switchRelay(bool switching)
        {
            int channel=1; // 1~8, for channel 1 control only by now

            var items = _relaysEnumerator.CollectInfo()
                .Select(x => new RelayItem(x))
                .ToArray();
            if (items.Length > 0)
            {
                _selectedRelay = new Relay(items[0].RelayInfo); // 1 relay only
                _selectedRelay.Open();
                //var channelsStatus = _selectedRelay.ReadChannels();
                _selectedRelay.WriteChannel(channel, switching);
                _selectedRelay.Close(); 
            }
        }

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].ToLower() == "on")
                {
                    switchRelay(true);
                }
                else
                {
                    switchRelay(false);
                }
            }
        }
    }
}
