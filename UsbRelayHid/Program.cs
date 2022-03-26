using System;
using System.Linq;
using UsbRelayNet.RelayLib;

namespace UsbRelayHid
{
    internal class Program
    {
        private static readonly RelaysEnumerator _relaysEnumerator = new RelaysEnumerator();
        private static Relay _selectedRelay=null;


        private static void switchRelay(bool switch2NO)
        {
            int channel=1; // 1~8, for channel 1 control only by now

            var items = _relaysEnumerator.CollectInfo()
                .Select(x => new RelayItem(x))
                .ToArray();
            if (items.Length > 0)
            {
                _selectedRelay = new Relay(items[0].RelayInfo); // 1 relay only
                _selectedRelay.Open();
                _selectedRelay.WriteChannel(channel, switch2NO);
                _selectedRelay.Close(); 
                Console.Write("USB Relay HID switch to ");
                if (switch2NO) 
                {
                    Console.WriteLine("NO");
                }
                else 
                {
                    Console.WriteLine("NC");
                }
            }
            else
            {
                Console.WriteLine("No USB Relay HOD found.");
            }
        }

        static void Main(string[] args)
        {
            //args[0]: [NC|NO]
            if (args.Length > 0)
            {
                if (args[0].ToUpper() == "NO")
                {
                    switchRelay(true);
                }
                else
                {
                    switchRelay(false);
                }
            }
            else
            {
                Console.WriteLine("Usage: UsbRelayHid NC|NO");
            }
        }
    }
}
