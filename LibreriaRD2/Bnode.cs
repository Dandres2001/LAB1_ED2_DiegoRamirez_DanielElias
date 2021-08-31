using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
namespace LibreriaRD2
{
   public class Bnode<T> where T: IComparable
    {
        public Bnode<T>[] punteros;

        public T[] Data;
 
        public static int orden { get; set; }
        public int gastados;

        public Bnode(int Orden)
        {
            orden = Orden;
            this.gastados = 0;
            punteros = new Bnode<T>[orden + 1];
            Data = new T[orden]; 
            for (int  i= 0; i< orden; i++)
            {
                punteros[i] = null;
                Data[i] = default(T);

                  
            }
            punteros[orden] = null;
        }

        public bool Estalleno()
        {

            return (gastados == (orden -1));
        }

        public bool isEmpty()
        {
            return (gastados == 0);
        }

        public bool isHalfEmpty()
        {
            return (gastados < orden / 2);
        }

        public bool isHalfFull()
        {
            return (gastados > orden / 2);
        }

        public bool esHoja()
        {
            for (int i = 0; i < orden; i++)
                if (punteros[i] != null)
                    return false;
            return true;
        }

        public void Aumentar()
        {
            this.gastados++;
        }

        public void Restar()
        {
            this.gastados--;
        }
        public Bnode<T> getApuntadores(int indice)
        {
            return punteros[indice];
        }
        public void setpunteros(int indice, Bnode<T> puntero)
        {
            this.punteros[indice] = puntero;
        }
        public T getData(int indice)
        {
            return Data[indice];
        }
        public void setData(int indice, T data)
        {
            this.Data[indice] = data;
        }
        public int getOrden()
        {
            return orden;
        }
        public int getGastados()
        {
            return gastados;
        }
        public void setGastados(int Gastados)
        {
            this.gastados = Gastados;
        }








    }
}
