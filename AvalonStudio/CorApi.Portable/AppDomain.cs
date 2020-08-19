﻿// Copyright (c) 2017 Vital Element Avalon Studio - Dan Walmsley dan at walms dot co dot uk
// 
// This code is licensed for use only with AvalonStudio. It is not permitted for use in any 
// project unless explicitly authorized.
//

using System;
using System.Collections.Generic;
using System.Text;

namespace CorApi.Portable
{
    public partial class AppDomain
    {
        /** The name of the CorAppDomain */
        public String Name
        {
            get
            {
                unsafe
                {
                    var count = 0u;
                    GetName(0, out count, IntPtr.Zero);

                    if (count == 0)
                    {
                        return null;
                    }

                    var temp = stackalloc char[(int)count];
                    GetName(count, out count, (IntPtr)temp);
                    
                    return new string(temp, 0, (int)count - 1);
                }
            }
        }

        public uint Id
        {
            get
            {
                GetID(out uint result);

                return result;
            }
        }

        public Process Process
        {
            get
            {
                Process proc = null;
                GetProcess(out proc);
                return Portable.Process.GetCorProcess(proc.NativePointer);
            }
        }
    }
}
