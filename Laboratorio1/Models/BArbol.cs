using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaseosaLab01.Models
{
    public class BArbol<TKey, T> where TKey : IComparable<TKey>
    {

        public BNodo<TKey, T> Raiz { get; set; }

        public int Grado { get; private set; }

        public int Altura { get; private set; }

   
        public BArbol(int grado)
        {
            if (grado < 2)
            {
                throw new ArgumentException("Arbol B debe ser de por lo menos grado 2", "grado");
            }
            this.Raiz = new BNodo<TKey, T>(grado);
            this.Grado = grado;
            this.Altura = 1;
        }

        // <returns>Valor buscado</returns>
        public Entry<TKey, T> Search(TKey Llave)
        {
            return this.BusquedaInterna(this.Raiz, Llave);
        }

        // Metodo para insertar una nueva llave junto con su objeto o tipo de dato

        public void Insertar(TKey nuevaLlave, T nuevoApuntador)
        {
            InsertarRecursivo(this.Raiz, nuevaLlave, nuevoApuntador, null);

        }


        private void InsertarRecursivo(BNodo<TKey, T> nodo, TKey nuevaLlave, T nuevoApuntador, BNodo<TKey, T> nodoPadre)
        {
            int posicionInsertar = nodo.Entradas.TakeWhile(entry => nuevaLlave.CompareTo(entry.LLave) >= 0).Count();
            //Es hoja
            if (nodo.EsHoja)
            {
                if (this.Raiz == nodo)
                {
                    this.Raiz.Entradas.Insert(posicionInsertar, new Entry<TKey, T>() { LLave = nuevaLlave, Apuntador = nuevoApuntador });
                    if (this.Raiz.AlcanzaMaximaEntrada)
                    {
                        // nuevo nodo y se necesita dividir
                        BNodo<TKey, T> viejaRaiz = this.Raiz;
                        this.Raiz = new BNodo<TKey, T>(this.Grado);
                        this.Raiz.Hijos.Add(viejaRaiz);
                        this.DividirHijo(this.Raiz, 0, viejaRaiz);
                        this.Altura++;
                    }
                    return;
                }
                else
                {
                    nodo.Entradas.Insert(posicionInsertar, new Entry<TKey, T>() { LLave = nuevaLlave, Apuntador = nuevoApuntador });
                    if (nodo.AlcanzaMaximaEntrada)
                    {
                        posicionInsertar = nodoPadre.Entradas.TakeWhile(entry => nuevaLlave.CompareTo(entry.LLave) >= 0).Count();
                        DividirHijo(nodoPadre, posicionInsertar, nodo);
                    }
                    return;
                }
            }
            //No es Hoja
            else
            {
                this.InsertarRecursivo(nodo.Hijos[posicionInsertar], nuevaLlave, nuevoApuntador, nodo);
                if (nodoPadre == null)
                {
                    if (nodo.AlcanzaMaximaEntrada)
                    {
                        BNodo<TKey, T> viejaRaiz = this.Raiz;
                        this.Raiz = new BNodo<TKey, T>(this.Grado);
                        this.Raiz.Hijos.Add(viejaRaiz);
                        this.DividirHijo(this.Raiz, 0, viejaRaiz);
                        this.Altura++;
                    }
                    return;
                }
                else
                {
                    if (nodo.AlcanzaMaximaEntrada)
                    {
                        DividirHijo(nodoPadre, posicionInsertar, nodo);
                    }
                    return;
                }
            }

        }
        // Metodo para realizar un busqueda interna entre nodos
        private Entry<TKey, T> BusquedaInterna(BNodo<TKey, T> node, TKey key)
        {
            int i = node.Entradas.TakeWhile(entry => key.CompareTo(entry.LLave) > 0).Count();

            if (i < node.Entradas.Count && node.Entradas[i].LLave.CompareTo(key) == 0)
            {
                return node.Entradas[i];
            }
            return node.EsHoja ? null : this.BusquedaInterna(node.Hijos[i], key);
        }
        private void DividirHijo(BNodo<TKey, T> padreNodo, int nodoCorrer, BNodo<TKey, T> nodoMover)
        {

            var nuevoNodo = new BNodo<TKey, T>(this.Grado);
            if (Grado % 2 == 0)
            {
                padreNodo.Entradas.Insert(nodoCorrer, nodoMover.Entradas[(this.Grado / 2) - 1]);
            }
            else
            {
                padreNodo.Entradas.Insert(nodoCorrer, nodoMover.Entradas[(this.Grado / 2)]);
            }

            if (Grado % 2 == 0)
            {
                nuevoNodo.Entradas.AddRange(nodoMover.Entradas.GetRange((this.Grado / 2), (this.Grado / 2)));
                nodoMover.Entradas.RemoveRange((this.Grado / 2) - 1, (this.Grado / 2) + 1);
            }
            else
            {
                nuevoNodo.Entradas.AddRange(nodoMover.Entradas.GetRange((this.Grado / 2) + 1, this.Grado / 2));
                nodoMover.Entradas.RemoveRange((this.Grado / 2), (this.Grado / 2) + 1);
            }
            if (!nodoMover.EsHoja)
            {
                if (Grado % 2 == 0)
                {
                    nuevoNodo.Hijos.AddRange(nodoMover.Hijos.GetRange((this.Grado / 2), (this.Grado / 2) + 1));
                    nodoMover.Hijos.RemoveRange((this.Grado / 2), (this.Grado / 2) + 1);
                }
                else
                {
                    nuevoNodo.Hijos.AddRange(nodoMover.Hijos.GetRange((this.Grado / 2) + 1, (this.Grado / 2) + 1));
                    nodoMover.Hijos.RemoveRange((this.Grado / 2) + 1, (this.Grado / 2) + 1);
                }
            }
            padreNodo.Hijos.Insert(nodoCorrer + 1, nuevoNodo);
        }
    }
}