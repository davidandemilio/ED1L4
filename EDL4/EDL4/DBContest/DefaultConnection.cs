using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDL4.Models;
namespace EDL4.DBContest
{
    public class DefaultConnection
    {
        private static volatile DefaultConnection Instance;
        private static object syncRoot = new Object();


        public Dictionary<string, Pais> DiccionarioListados = new Dictionary<string, Pais>();


        public Dictionary<string, Estampa> estampasdirectas = new Dictionary<string, Estampa>();

        public List<Estampa> estampas = new List<Estampa>();


        public int IDActual { get; set; }

        private DefaultConnection()
        {
            IDActual = 0;
        }

        public static DefaultConnection getInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Instance == null)
                        {
                            Instance = new DefaultConnection();
                        }
                    }
                }
                return Instance;
            }
        }
    }
}