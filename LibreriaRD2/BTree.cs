using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;


namespace LibreriaRD2
{


    public class BTree<T> where T : IComparable 
    {
       

        private static int orden { get; set; }
        public Bnode<T> root;

     

        T medium;
        Bnode<T> TempNode;
      
        bool aumentarnivel;
        bool found;
        private int nivel;
        private int nodos;
        public List<T> traversal = new List<T>();

        public BTree(int Orden )
        {
            orden = Orden;
            root = null;
            nodos = 0;
            nivel = 0;

        }
        public void insert(T data)
        {
          
            this.root= insert(this.root, data);
            this.nodos++;
        }


        public Bnode<T> insert(Bnode<T> actual, T data)
        {
            push(actual, data);
            if (aumentarnivel)
            {
                Bnode<T> temporal = new Bnode<T>(orden);
               
                temporal.Aumentar();
                temporal.setData(0, medium);
                temporal.setpunteros(0, this.root);
                 
              
                temporal.setpunteros(1, TempNode);
                this.root = temporal;
                this.nivel++;
                
                
            }
           
     
                return this.root;
        }
        public void push(Bnode<T> current, T data)
        {
            int j = 0;
            found = false;
            if (current == null)
            {
                aumentarnivel = true;
                this.medium = data;
                TempNode = null;
           
            }
            else
            {
                j = SearchNode(data, current);
                if (found)
                {
                    aumentarnivel = false;
                }
                else
                {
                    push(current.getApuntadores(j), data);
                    if (aumentarnivel)
                    {
                        if (current.Estalleno())
                        {
                            aumentarnivel = true;
                            splitnode(this.medium, current, j);
                        }
                        else
                        {
                            aumentarnivel = false;
                            addData(this.medium, current, j);
                        }
                    }
                }
            }

        }
        public void addData(T data, Bnode<T> current, int  index)
        {
            Bnode<T> currentapuntadores = new Bnode<T>(orden);
            int b = 0;
            for (int i = 0; i < orden; i++)
            {
                currentapuntadores.setpunteros(i, current.getApuntadores(i));
            }
            for (int i= current.getGastados(); i != index; i--)
            {
               
                current.setData(i, current.getData(i - 1));
               
                current.setpunteros(i + 1, current.getApuntadores(i));

                b++; 


            }

            current.setData(index, data);
            
            current.setpunteros(index + 1, TempNode);
            if (b >1)
            {
                for (int i = 0; i < orden; i++)
                {
                    if  (i== index+1)
                    {
                        i = i + 1;
                    }
                    current.setpunteros(i+1, currentapuntadores.getApuntadores(i));
                }
                current.setpunteros(index + 1, TempNode);
            }
                current.Aumentar();
        }

        public void splitnode(T data, Bnode<T> current, int index)
        {
            int middleposition = (index <= (orden / 2)) ? orden / 2 : (orden / 2) + 1;

            Bnode<T> NewNode = new Bnode<T>(orden);
    
       
          

            for (int  pos = middleposition +1; pos <orden; pos++)
            {
                NewNode.setData((pos - middleposition) - 1, current.getData(pos - 1));
                NewNode.setpunteros((pos - middleposition) - 1, current.getApuntadores(pos));

            }
       
            NewNode.setGastados(orden - 1 - middleposition);
            current.setGastados(middleposition);

            if (index <= (orden / 2))
            {
                addData(data, current, index);
            }
            else
            {
                addData(data, NewNode, index - middleposition);
                    
            }
            medium = current.getData(current.getGastados() - 1);


            int position = middleposition;
            current.setGastados(current.getGastados() - 1);
            for (int pos = orden; pos >(middleposition); pos--)
            {
             
                NewNode.setpunteros(position, current.getApuntadores(pos));
                position--;
            }
            for  (int  i = (orden)/2; i < orden ; i++)
            {
                current.setData(i, default(T));
            

            }
            for (int i =0; i <(middleposition+1 ); i++)
            {
                current.setpunteros(i, current.getApuntadores(i));
            }

            for  (int i = orden; i > (middleposition); i--)
            {
                current.setpunteros(i, null);
            }

            //current.setpunteros(orden, null);
            TempNode = NewNode;
      
        }
        public int SearchNode(T data, Bnode<T> currrent)
        {
            int j = 0;
            if (currrent.Data[0].CompareTo(data) > 0)
            {
                found = false;
                j = 0; 

            }
            else
            {
                j = currrent.getGastados();
              
                while (currrent.Data[j-1].CompareTo(data) >0 && j > 1)
                {
                    
                    --j;
                    found = (currrent.getData(j - 1).CompareTo(data) == 0);
                }
                if (currrent.getData(j - 1).CompareTo(data) == 0)
                {
                    found = true;
                }

            }
            return j; 
        }

        public T searchbydata(Bnode<T> current, T data, T temp)
        {
            if  (current != null)
            {
                for (int i=0; i<current.getGastados(); i++)
                {
                    if (current.getData(i).CompareTo(data) == 0)
                    {
                        temp = current.getData(i);
                    }
                 
                }
                for (int i = 0; i <= current.getGastados(); i++)
                {
                    temp = searchbydata(current.getApuntadores(i), data, temp);
                }
            }
         
            return temp; 
        }
   
        public void delete(T data)
        {

            if (this.root != null)
            {
                delete(this.root, data);

            }
        }
        public void delete(Bnode<T> current, T data)
        {
            try
            {
                deletedata(current, data);
            }
            catch(Exception e)
            {
                found = false; 
            }
            if (found)
            {
                found = false; 
                if (current.getGastados() == 0)
                {
                    current = current.getApuntadores(0);
                    this.root = current;
                }
            }
        }

        public void deletedata(Bnode<T> current, T data)
        {
            int pos = 0;
        
            if  (current == null)
            {
                found = false; 
            }
            else
            {
                pos = SearchNode(data, current);
                if (found)
                {
                    if (current.getApuntadores(pos-1) == null)
                    {
                        remove(current, pos);
                        
                    }
                    else
                    {
                        sucesor(current, pos);
                        deletedata(current.getApuntadores(pos), current.getData(pos - 1));
                    }
                }
                else
                {
                    deletedata(current.getApuntadores(pos), data);
                    if (current.getApuntadores(pos) != null && current.getApuntadores(pos).isHalfEmpty())
                    {
                        reset(current, pos);
                    }
                }
            }
        }


        public void remove (Bnode<T> current, int  pos)
        {
            bool comprobar = false;
            for (int i = pos +1; i != current.getGastados() +1; i++)
            {
                comprobar = true;
                current.setData(i - 2, current.getData(i - 1));
                current.setData(i - 1, default(T));
                current.setpunteros(i - 1, current.getApuntadores(i));
                current.setpunteros(i, null);
            }
            if (comprobar == false)
            {
                current.setData(pos - 1, current.getData(pos));
            }
            current.Restar();
        }
        public void sucesor  (Bnode<T> current, int index)
        {
            Bnode<T> temp = new Bnode<T>(orden);
             temp.setpunteros(0,current.getApuntadores(index));
            while (temp.getApuntadores(0) != null)
            {
                temp = temp.getApuntadores(0);
                current.setData(index - 1, temp.getData(0));

            }
        }

        public void reset(Bnode<T> current, int pos)
        {
            if (pos > 0)
            {
                if (current.getApuntadores(pos - 1).isHalfFull())
                {
                    ToTheRight(current, pos);
                }
                else
                {
                    LinkData(current, pos); 
                }
            }
            else
            {
                if (current.getApuntadores(orden / 2 - 1).isHalfFull())
                {
                    ToTheLeft(current, orden / 2 - 1);
                }
                else
                {
                    LinkData(current, orden / 2 - 1);
                }
            }
        }

        public void ToTheRight(Bnode<T> current, int pos )
        {
            Bnode<T> temp = current.getApuntadores(pos);
            for (int  i = temp.getGastados(); i>0; i--)
            {
                temp.setData(i, temp.getData(i - 1));
                temp.setpunteros(i + 1, temp.getApuntadores(i));
            }
            temp.Aumentar();
            temp.setpunteros(1, temp.getApuntadores(0));
            temp.setData(0, current.getData(pos - 1));
            current.setData(pos - 1, current.getApuntadores(pos - 1).getData(current.getApuntadores(pos-1).getGastados()-1));
            temp.setpunteros(0, current.getApuntadores(pos - 1).getApuntadores(current.getApuntadores(pos - 1).getGastados()));
            current.getApuntadores(pos - 1).Restar();
        }

        public void ToTheLeft(Bnode<T> current, int pos)
        {
            Bnode<T> temp = current.getApuntadores(pos - 1);
            temp.Restar();
            temp.setData(temp.getGastados() - 1, current.getData(pos - 1));
            temp.setpunteros(temp.getGastados(), current.getApuntadores(pos).getApuntadores(0));
            current.setData(pos - 1, current.getApuntadores(pos).getData(0));
            current.getApuntadores(pos).setpunteros(0, current.getApuntadores(pos).getApuntadores(1));
            current.getApuntadores(pos).Restar();

            temp = current.getApuntadores(pos);
            for(int i =1; i != temp.getGastados() +1; i++)
            {
                temp.setData(i - 1, temp.getData(i));
                temp.setpunteros(i, temp.getApuntadores(i + 1));
            }
        }
        public void LinkData(Bnode<T> current, int pos)
        {
            Bnode<T> right = current.getApuntadores(pos);
            Bnode<T> left = current.getApuntadores(pos - 1);
            if (left.getGastados() > 1)
            {
                left.Restar();
            }
            left.setData(left.getGastados() , current.getData(pos - 1));
            left.setpunteros(left.getGastados(), right.getApuntadores(0));
            
            for (int i =0; i != right.getGastados(); i++)
            {
               
                //if (left.getData(i).CompareTo(0) == 0)
                //{
                    left.Aumentar();
                    left.setData(left.getGastados(), right.getData(i));
                    left.setpunteros(left.getGastados(), right.getApuntadores(i));
                //}
            }
            remove(current, pos);
        }


        public static int getorder()
        {
            return orden;
        }
        public Bnode<T> getroot()
        {
            return root; 
        }


        public void setroot(Bnode<T> root)
        {
            this.root = root;
        }

        bool isEmpty()
        {
            return (root == null);
        }

        public int getnivel()
        {
            return nivel;
        }

        public void setnivel(int nivel)
        {
            this.nivel = nivel; 
        }

        public int getnodes()
        {
            return nodos; 
        }

        public List<Bnode<T>> recorrido = new List<Bnode<T>>();

        public List<Bnode<T>> recorrer()
        {
            recorrido.Clear();
            recorrer(recorrido, root);
            return recorrido; 
        }

        private void recorrer(List<Bnode<T>> temp, Bnode<T> hoja)
        {
            if (hoja != null)
            {
                temp.Add(hoja);
                for (int i = 0; i<orden; i++)
                {
                    recorrer(temp, hoja.getApuntadores(i));
                }
            }
        }
        public void inorder(Bnode<T> current)
        {
            
            //int i; 
            if (current != null)
            {
                for (int i = 0; i < current.getGastados() + 1; i++)
                {

                    inorder(current.getApuntadores(i));
                    if (i < current.getGastados())
                    {
                        traversal.Add(current.getData(i));
                    }
                }

            }

        }
        public void preorden(Bnode<T> current)
        {
           
            if (current != null)
            {
                for (int i = 0; i < current.getGastados() + 1; i++)
                {


                    if (i < current.getGastados())
                    {
                        traversal.Add(current.getData(i));
                    }
                    preorden(current.getApuntadores(i));
                }
            }
        }

        public void postorden(Bnode<T> current)
        {
            
            if (current != null)
            {
                postorden(current.getApuntadores(0));
                for (int i = 0; i < current.getGastados() + 1; i++)
                {
                    postorden(current.getApuntadores(i + 1));
                    if (i < current.getGastados())
                    {
                        traversal.Add(current.getData(i));
                    }




                }
            }
        }


    }

}
