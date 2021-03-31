/*
    Kingdom Save Editor
    Copyright (C) 2020 Luciano Ciccariello

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using KHSave.Archives.Factories;
using System.IO;

namespace KHSave.Archives
{
    public static class ArchiveFactories
    {
        public static IArchiveFactory Ps4Kh1 = new Ps4Kh1Factory();
        public static IArchiveFactory Ps4Kh2 = new Ps4Kh2Factory();
        public static IArchiveFactory Ps4KhRecom = new Ps4KhRecomFactory();
        public static IArchiveFactory Ps4KhDdd = new Ps4KhDddFactory();
        public static IArchiveFactory PcKh1 = new PcKh1Factory();
        public static IArchiveFactory PcKh2 = new PcKh2Factory();
        public static IArchiveFactory PcKhRecom = new PcKhRecomFactory();


        public static bool TryGetFactory(Stream stream, out IArchiveFactory archiveFactory)
        {
            if (Ps4Kh1.IsValid(stream))
                archiveFactory = Ps4Kh1;
            else if (Ps4Kh2.IsValid(stream))
                archiveFactory = Ps4Kh2;
            else if (Ps4KhRecom.IsValid(stream))
                archiveFactory = Ps4KhRecom;
            else if (Ps4KhDdd.IsValid(stream))
                archiveFactory = Ps4KhDdd;
            else if (PcKh1.IsValid(stream))
                archiveFactory = PcKh1;
            else if (PcKh2.IsValid(stream))
                archiveFactory = PcKh2;
            else if (PcKhRecom.IsValid(stream))
                archiveFactory = PcKhRecom;
            else
                archiveFactory = null;

            return archiveFactory != null;
        }
    }
}
