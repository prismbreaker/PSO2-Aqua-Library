﻿using SoulsFormats.Formats.Morpheme.MorphemeBundle.Network.NodeAttrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsFormats.Formats.Morpheme.MorphemeBundle.Network
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class NodeDataSet
    {
        //public long attribOffset;
        public NodeAttribBase attrib;
        public long size;
        public int alignment;
        public int iVar0;
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
