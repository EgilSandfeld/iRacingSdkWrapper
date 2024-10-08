﻿using System;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace iRSDKSharp
{
    public class CVarBuf
    {
        public const int VarHeaderOffset = 56;
        public int VarHeaderSize = 144;
        //VarBuf offsets
        public int VarBufOffset = 48;
        public const int VarTickCountOffset = 0;
        public const int VarBufOffsetOffset = 4;
        public int VarBufSize = 32;

        MemoryMappedViewAccessor FileMapView = null;
        CiRSDKHeader Header = null;

        public CVarBuf(MemoryMappedViewAccessor mapView, CiRSDKHeader header)
        {
            FileMapView = mapView;
            Header = header;
            VarHeaderSize = Marshal.SizeOf(typeof(VarHeader));
            VarBufSize = Marshal.SizeOf(typeof(VarBuf));
        }

        public int OffsetLatest
        {
            get
            {
                try
                {
                    int bufCount = Header.BufferCount;
                    int[] ticks = new int[Header.BufferCount];
                    for (int i = 0; i < bufCount; i++)
                    {
                        ticks[i] = FileMapView.ReadInt32(VarBufOffset + ((i * VarBufSize) + VarTickCountOffset));
                    }

                    if (ticks.Length == 0)
                        return 0;
                    
                    int latestTick = ticks[0];
                    int latest = 0;
                    for (int i = 0; i < bufCount; i++)
                    {
                        if (latestTick < ticks[i])
                        {
                            latest = i;
                        }
                    }
                    return FileMapView.ReadInt32(VarBufOffset + ((latest * VarBufSize) + VarBufOffsetOffset));
                }
                catch (IndexOutOfRangeException)
                {
                    return 0;
                }
            }
        }
    }
}
