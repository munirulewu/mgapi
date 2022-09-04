/*
 * File name            : ASIBLXoomEntityCollectionBase.cs
 * Author               : Munirul Islam
 * Date                 : November 10, 2014
 * Version              : 1.0
 *
 * Description          : Base class for all Entity objects
 *
 * Modification history :
 * Name                         Date                            Desc
 *    
 * 
 * Copyright (c) 2014: SOCIAL ISLAMI BANK LIMITED
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SIBLCommon.Common.Entity.Bases
{
    [Serializable]
    public class ASIBLEntityCollectionBase : ISIBLEntityBase, ICollection
    {
        #region Protected Members

        protected String m_sOpenStringVal;

        #endregion Protected Members

        #region Constructor
        public ASIBLEntityCollectionBase()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sOpenStringVal = "";
        }
        #endregion Initialization

        #region ICollection Members

        public String OpenStringValue
        {
            get { return m_sOpenStringVal; }
            set { m_sOpenStringVal = value; }
        }

        public void CopyTo(Array array, int index)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Count
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public bool IsSynchronized
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public object SyncRoot
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo(Array array, int index)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        int ICollection.Count
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        bool ICollection.IsSynchronized
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        object ICollection.SyncRoot
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
