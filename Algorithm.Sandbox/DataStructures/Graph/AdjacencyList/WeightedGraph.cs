﻿using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures.Graph.AdjacencyList
{
    /// <summary>
    /// A weighted graph vertex
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    public class WeightedGraphVertex<T, W> where W : IComparable
    {
        public T Value { get; private set; }

        public Dictionary<WeightedGraphVertex<T, W>, W> Edges { get; set; }

        public WeightedGraphVertex(T value)
        {
            Value = value;
            Edges = new Dictionary<WeightedGraphVertex<T, W>, W>();

        }

    }

    /// <summary>
    /// A weighted graph implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    public class AsWeightedGraph<T, W> where W : IComparable
    {
        public int VerticesCount => Vertices.Count;
        internal Dictionary<T, WeightedGraphVertex<T, W>> Vertices { get; set; }

        /// <summary>
        /// return a reference vertex
        /// </summary>
        public WeightedGraphVertex<T, W> ReferenceVertex
        {
            get
            {
                var enumerator = Vertices.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    return enumerator.Current.Value;
                }

                return null;
            }
        }


        public AsWeightedGraph()
        {
            Vertices = new Dictionary<T, WeightedGraphVertex<T, W>>();
        }

        /// <summary>
        /// Add a new vertex to this graph
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WeightedGraphVertex<T, W> AddVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            var newVertex = new WeightedGraphVertex<T, W>(value);

            Vertices.Add(value, newVertex);

            return newVertex;
        }

        /// <summary>
        /// remove given vertex from this graph
        /// </summary>
        /// <param name="value"></param>
        public void RemoveVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (!Vertices.ContainsKey(value))
            {
                throw new Exception("Vertex not in this graph.");
            }


            foreach (var vertex in Vertices[value].Edges)
            {
                if (!Vertices.ContainsKey(vertex.Key.Value))
                {
                    throw new Exception("Vertex outgoing edge target vertex is not in this graph.");
                }

                vertex.Key.Edges.Remove(Vertices[value]);
            }

            Vertices.Remove(value);
        }

        /// <summary>
        /// Add a new edge to this graph with given weight 
        /// and between given source and destination vertex
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="weight"></param>
        public void AddEdge(T source, T dest, W weight)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }


            Vertices[source].Edges.Add(Vertices[dest], weight);
            Vertices[dest].Edges.Add(Vertices[source], weight);
        }

        /// <summary>
        /// Remove given edge
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public void RemoveEdge(T source, T dest)
        {

            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (!Vertices[source].Edges.ContainsKey(Vertices[dest]) 
                || !Vertices[dest].Edges.ContainsKey(Vertices[source]))
            {
                throw new Exception("Edge do not exists partially or totally.");
            }

            Vertices[source].Edges.Remove(Vertices[dest]);
            Vertices[dest].Edges.Remove(Vertices[source]);
        }

        /// <summary>
        /// Do we have an edge between given source and destination
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public bool HasEdge(T source, T dest)
        {
            if(!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            {
                throw new ArgumentException("source or destination is not in this graph.");
            }

            var sourceExists = false;

            foreach (var edge in Vertices[source].Edges)
            {
                if (edge.Key == Vertices[dest])
                {
                    sourceExists = true;
                    break;
                }
            }

            var destExists = false;

            foreach (var edge in Vertices[dest].Edges)
            {
                if (edge.Key == Vertices[source])
                {
                    destExists = true;
                    break;
                }
            }

            return sourceExists && destExists;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WeightedGraphVertex<T, W> FindVertex(T value)
        {
            if (Vertices.ContainsKey(value))
            {
                return Vertices[value];
            }

            return null;
        }
    }
}
