﻿using AquaModelLibrary.AquaMethods;
using Reloaded.Memory.Streams;
using System.Collections.Generic;
using System.Numerics;

namespace AquaModelLibrary.Extra.AM2
{
    //Stage layouts
    public unsafe class STG
    {
        public STGHeader header;
        public List<STGObjectClass> objList = new List<STGObjectClass>(); //Should be unique?

        public STG()
        {

        }

        public STG(BufferedStreamReader sr)
        {
            header = sr.Read<STGHeader>();

            sr.Seek(header.objectsOffset, System.IO.SeekOrigin.Begin);
            for (int i = 0; i < header.objectCount; i++)
            {
                objList.Add(new STGObjectClass(sr));
            }
        }

        public struct STGHeader
        {
            public long objectCount;
            public long objectsOffset;
            public fixed long unusedBuffer[14];
        }

        public struct STGObject
        {
            public Matrix4x4 transform;
            public long unknownId_40;
            public long objectTransformSetOffset;

            public long objectPropertyOffset;
            public long objectNameOffset;

            public long unknownFloatPairOffset;
        }

        public struct UnknownFloatPair
        {
            public float flt_00;
            public float flt_04;
        }

        public struct UnkPlaneHeader
        {
            public long setCount;
            public long setOffset;
        }

        public struct UnkPlane
        {
            public Vector3 directionVector;
            public Vector3 vec3_0C;
            public Vector3 vec3_18;
            public Vector3 vec3_24;
            public Vector3 vec3_30;
        }

        public class STGObjectClass
        {
            public STGObject stgObj;
            public UnkPlaneHeader unkPlaneHeader;
            public List<UnkPlane> unkPlanes = new List<UnkPlane>();
            public string modelName = null; //The property is the model name if it's not null
            public string objName = null;
            public STGObjectClass()
            {

            }

            public STGObjectClass(BufferedStreamReader sr)
            {
                stgObj = sr.Read<STGObject>();
                var bookmark = sr.Position();

                if (stgObj.objectTransformSetOffset != 0)
                {
                    sr.Seek(stgObj.objectTransformSetOffset, System.IO.SeekOrigin.Begin);
                    unkPlaneHeader = sr.Read<UnkPlaneHeader>();

                    sr.Seek(unkPlaneHeader.setOffset, System.IO.SeekOrigin.Begin);
                    for (int i = 0; i < unkPlaneHeader.setCount; i++)
                    {
                        unkPlanes.Add(sr.Read<UnkPlane>());
                    }
                }

                if (stgObj.objectPropertyOffset != 0)
                {
                    sr.Seek(stgObj.objectPropertyOffset, System.IO.SeekOrigin.Begin);
                    modelName = AquaGeneralMethods.ReadCString(sr);
                }

                sr.Seek(stgObj.objectNameOffset, System.IO.SeekOrigin.Begin);
                objName = AquaGeneralMethods.ReadCString(sr);

                sr.Seek(bookmark, System.IO.SeekOrigin.Begin);
            }

        }
    }
}
