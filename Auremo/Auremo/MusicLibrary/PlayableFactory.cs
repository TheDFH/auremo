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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auremo.MusicLibrary
{
    public static class PlayableFactory
    {
        public static Playable CreatePlayable(MPDSongResponseBlock block, DataModel dataModel = null)
        {
            Path path = new Path(block.File);

            if (path.IsStream)
            {
                return CreateAudioStream(path, "", block);
            }
            else if (dataModel == null)
            {
                return CreateLink(path, block);
            }
            else if (!path.CanBeLocal)
            {
                return CreateLink(path, block);
            }

            return FetchSong(path, block, dataModel.Database);
        }

        public static Song FetchSong(Path path, MPDSongResponseBlock block, Database database)
        {
            Song song = null;

            if (database.Songs.TryGetValue(path, out song))
            {
                return song;
            }
            else
            {
                throw new Exception("PlayableFactory.FetchSong(): expected to find \"" + block.File + "\" in library, didn't.");
            }
        }

        public static AudioStream CreateAudioStream(Path path, string label, MPDSongResponseBlock block)
        {
            AudioStream result = new AudioStream(path, label);
            result.Title = block.Title;
            result.Name = block.Name;
            return result;
        }

        public static Link CreateLink(Path path, MPDSongResponseBlock block)
        {
            Link result = new Link(path);
            result.Title = block.Title;
            result.Artist = block.Artist;
            result.Album = block.Album;
            return result;
        }
    }
}
