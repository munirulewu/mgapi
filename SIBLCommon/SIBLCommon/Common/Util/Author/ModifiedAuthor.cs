/*
 * File name            : ModifiedAuthor.cs
 * Author               : Munirul Islam
 * Date                 : 28 December 2014
 * Version              : 1.0
 *
 * Description          : This is Modified Auther Class object
 *
 * Modification history :
 * Name                  Date                            Desc
 * 
 * 
 * Copyright (c) 2014: SOCIAL ISLAMI BANK LIMITED
 */

using System;
using System.Reflection;

namespace SIBLCommon.Common.Util.Attributes
{
    [Author("MI", "28-December-2014", "The purpose to develop the class is to create modified author attribute of element.")]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ModifiedAuthor:Attribute
    {
        private String m_sModifiedAuthor;
        private DateTime m_dModifiedDate;
        private String m_sPurpose;

        [Author("MI", "28-December-2014", "To protect to create instance of the class with parameter.")]
        private ModifiedAuthor() { }

        [Author("MI", "28-December-2014", "To initlize the modified author .")]
        public ModifiedAuthor(String author, string date, String purpose)
        {
            this.m_sModifiedAuthor = author;
            this.m_dModifiedDate = Convert.ToDateTime(date); 
            this.m_sPurpose = purpose;
        }

        [Author("MI", "28-December-2014", "To get the name of modified author.")]
        public String GetAuthorName
        {
            get { return this.m_sModifiedAuthor; }
        }

        [Author("MI", "28-December-2014", "To get the date of modification.")]
        public DateTime GetModifiedDate
        {
            get { return this.m_dModifiedDate; }
        }

        [Author("MI", "28-December-2014", "To get the purpose of modification.")]
        public String GetPurpose
        {
            get { return this.m_sPurpose; }
        }
    }
}
