using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OCR
{
    public class OSS_Access_Info
    {
        public string accessid { get; set; }
        public string policy { get; set; }
        public string signature { get; set; }
        public string dir { get; set; }
        public string host { get; set; }
        public string expire { get; set; }
        public string cdn { get; set; }
    }
}
