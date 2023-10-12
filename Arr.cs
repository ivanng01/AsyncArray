using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Asinc
{

    public class Arr
    {
        private object block = new object();
        private int[] NuevoArray;
        private int nAleatorio;
        private Random Aleatorio;
        

        public Arr(int tamA, int topeAleatorio)
        {
            this.NuevoArray = new int[tamA];

            this.nAleatorio = topeAleatorio;

            this.Aleatorio = new Random();
        }

        public void CompletarArray()
        {
            lock (block)
            {
                for (int i = 0; i < NuevoArray.Length; i++)
                {
                    NuevoArray[i] = Aleatorio.Next(0, nAleatorio + 1);
                    Console.WriteLine(" "+NuevoArray[i]+" - "  + " posición: " + i);
                }
            }
        }

        public async Task<int> EncontrarMayorPMitadAsync()
        {
            lock (block)
            {
                var priMit = NuevoArray.Length / 2;
                var mayorPriMit = NuevoArray[0];
                for (var i = 0; i < priMit; i++)
                {
                    if (NuevoArray[i] >= mayorPriMit)
                    {
                        mayorPriMit = NuevoArray[i];
                    }
                }
                return mayorPriMit;
            }
        }

        public async Task<int> EncontrarMayorSMitadAsync()
        {
            lock (block)
            {
                var segMit = NuevoArray.Length / 2;
                var mayorSegMit = NuevoArray[segMit]; 
                for (var i = segMit; i < NuevoArray.Length; i++)
                {
                    if (NuevoArray[i] >= mayorSegMit)
                    {
                        mayorSegMit = NuevoArray[i];
                    }
                }
                return mayorSegMit;
            }
        }

        public async Task<int> ValorMayorEnArray()
        {
            Task<int> mayPMitad = EncontrarMayorPMitadAsync();
            Task<int> maySMitad = EncontrarMayorSMitadAsync();
            await Task.WhenAll(mayPMitad, maySMitad);
            int pmArr = await mayPMitad;
            int smArr = await maySMitad;

            if (pmArr > smArr)
            { 
                Console.WriteLine("Mayor Valor en Primera Mitad: " + pmArr);
                return pmArr;
            }
            else
            { 
                Console.WriteLine("Mayor Valor en Segunda Mitad: " + smArr);
                return smArr;
            }
        }

        /* cod comprob
        public int PrimeraPos()
        {
            lock (block)
            {
                int posprim = 0;
                var priMit = NuevoArray.Length / 2;
                var mayorPriMit = NuevoArray[0];
                for (var i = 0; i < priMit; i++)
                {
                    if (NuevoArray[i] >= mayorPriMit)
                    {
                        mayorPriMit = NuevoArray[i];
                        posprim = i;
                    }
                }
                return posprim;
            }
        }

        public int SegundaPos()
        {
            lock (block)
            {
                int posseg = 0;
                var segMit = NuevoArray.Length / 2;
                var mayorSegMit = NuevoArray[segMit]; //ver de colocar 0 si se soluciona
                for (var i = segMit; i < NuevoArray.Length; i++)
                {
                    if (NuevoArray[i] >= mayorSegMit)
                    {
                        mayorSegMit = NuevoArray[i];
                        posseg = i;
                    }
                }
                return posseg;
            }
        }

        public void mostrarPosicion() 
        {
            Console.WriteLine("Mayor de Primera posicion: "+PrimeraPos());
            Console.WriteLine("Mayor de Segunda posicion: " +SegundaPos());
        }
        */
        
    }   
}


    