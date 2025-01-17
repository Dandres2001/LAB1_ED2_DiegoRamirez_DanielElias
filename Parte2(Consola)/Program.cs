﻿using System;
using LibreriaRD2;


namespace Parte2_Consola_
{
    class Program
    {
        static void Main(string[] args)
        {


            string inorden ="";
            string orden = "";
            string orden2 = ""; 
            BTree<int> arbol = new BTree<int>(5);
           
            arbol.insert(10);
            arbol.insert(54);
            arbol.insert(25);
            arbol.insert(81);
            arbol.insert(86);
            arbol.insert(87);
  
            arbol.insert(9);
            arbol.insert(74);
            arbol.insert(51);
            arbol.insert(47);
            //aqui
            arbol.insert(46);
            arbol.insert(12);
            arbol.insert(16);
            arbol.insert(34);
            arbol.insert(36);
            arbol.insert(96);
            arbol.insert(44);
            arbol.insert(6); //ERROR AQUí
            arbol.insert(19);
            arbol.insert(64);
            arbol.insert(21);
            arbol.insert(60);
            arbol.insert(50);
            arbol.insert(90);
        string prueba =    arbol.searchbydata(arbol.root, 60, 0).ToString();

            Console.WriteLine(prueba);

            arbol.delete(51);
            //arbol.delete(34);
            //arbol.delete(10);
            //arbol.delete(12);
            //arbol.delete(81);
            //arbol.delete(60);
            //arbol.delete(90);
            //arbol.delete(46);
            //arbol.insert(1);





            void inorder( Bnode<int> current)
            {
                //int i; 
                if (current != null)
                {
                    for (int i = 0; i < current.getGastados() + 1; i++)
                    {

                        inorder(current.getApuntadores(i));
                        if (i < current.getGastados())
                        {
                           inorden += current.getData(i) + " ";
                        }
                    }
               
                }
           
            }
            void preorden(Bnode<int> current)
            {
                if (current != null)
                {
                    for (int i = 0; i < current.getGastados() + 1; i++)
                    {

                       
                        if (i < current.getGastados())
                        {
                           orden+= current.getData(i) + " ";
                        }
                       preorden(current.getApuntadores(i));
                    }
                }
            }

            void postorden (Bnode<int> current)
            {
            
                if (current != null)
                {
                    postorden(current.getApuntadores(0));
                    for (int i = 0; i < current.getGastados() + 1; i++)
                    {
                        postorden(current.getApuntadores(i + 1));
                        if (i < current.getGastados())
                        {
                            orden2 += current.getData(i) + " ";
                        }
                
                        


                    }
                }
            }
            inorder(arbol.root);
            preorden(arbol.root);
            postorden(arbol.root);
            Console.WriteLine("en orden: " + inorden);
            Console.WriteLine("en pre orden: " + orden);
            Console.WriteLine("en post orden " + orden2);
            Console.ReadKey(); 

            


        }
      
    }
}
