/*
 * File name            : Author.cs
 * Author               : Munirul Islam
 * Date                 : 28 December 2014
 * Version              : 1.0
 *
 * Description          : This is Auther Class object
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2014: SOCIAL ISLAMI BANK LIMITED
 */

using System;
using System.Reflection;

namespace SIBLCommon.Common.Util.Attributes
{
    [Author("MI", "28-December-2014", "The purpose to develop the class is to create author attribute of element.")]
    [AttributeUsage(AttributeTargets.All, AllowMultiple=true)]
    public class Author:Attribute
    {
        private String m_sAuthor;
        private DateTime m_dDelvelopmentDate;
        private String m_sPurpose;

        [Author("MI", "28-December-2014", "To protect to create instance without parameter.")]
        private Author() { }

        [Author("MI", "28-December-2014", "To Initialize author.")]
        public Author(String author,string date, String purpose)
        {
            this.m_sAuthor=author;
            this.m_dDelvelopmentDate = Convert.ToDateTime(date);
            this.m_sPurpose = purpose;
        }

        [Author("MI", "28-December-2014", "To get the name of author.")]
        public String GetAuthorName
        {
            get { return this.m_sAuthor; }
        }

        [Author("MI", "28-December-2014", "To get the date of development.")]
        public DateTime GetDevelopmentDate
        {
            get { return this.m_dDelvelopmentDate; }
        }

        [Author("MI", "28-December-2014", "To get the purpose of development.")]        
        public String GetPurpose
        {
            get { return this.m_sPurpose; }
        }
    }
}
