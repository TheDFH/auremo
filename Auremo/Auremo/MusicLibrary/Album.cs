﻿/*
 * Copyright 2014 Mikko Teräs and Niilo Säämänen.
 *
 * This file is part of Auremo.
 *
 * Auremo is free software: you can redistribute it and/or modify it under the
 * terms of the GNU General Public License as published by the Free Software
 * Foundation, version 2.
 *
 * Auremo is distributed in the hope that it will be useful, but WITHOUT ANY
 * WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR
 * A PARTICULAR PURPOSE. See the GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License along
 * with Auremo. If not, see http://www.gnu.org/licenses/.
 */

using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Auremo.MusicLibrary
{
    public class Album : LibraryItem, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

        public static readonly string Unknown = "Unknown Album";
        ImageSource m_Cover = null;

        public Album(Artist artist, string title, string date, ImageSource cover = null)
        {
            Artist = artist;
            Title = title;
            Date = date;
            Cover = cover;
        }

        public Artist Artist
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Date
        {
            get;
            set;
        }

        public string Year
        {
            get
            {
                return Utils.ExtractYearFromDateString(Date);
            }
        }

        public ImageSource Cover
        {
            get
            {
                return m_Cover;
            }
            set
            {
                if (value != m_Cover)
                {
                    m_Cover = value;
                    NotifyPropertyChanged("Cover");
                }
            }
        }

        public override string DisplayString
        {
            get
            {
                return Title;
            }
        }

        public override int CompareTo(object o)
        {
            if (o is Album)
            {
                Album rhs = o as Album;
                int result = Artist.CompareTo(rhs.Artist);

                if (result == 0)
                {
                    result = StringComparer.Ordinal.Compare(Title, rhs.Title);
                }

                return result;
            }
            else
            {
                throw new Exception("Album: attempt to compare to an incompatible object");
            }
        }

        public override string ToString()
        {
            return DisplayString;
        }
    }
}
