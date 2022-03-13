using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BitString {
    // Define a bitstring class just for easy hashing, with operator * that is just a xor.
    public int Code { get; private set; }
    public BitString(bool random) {
        if (random) {
            Code = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        } else {
            Code = 0;
        }
    }

    public BitString(int code) {
        this.Code = code;
    }

    public override string ToString() {
        string binary = Convert.ToString(Code, 2);
        return binary;
    }

    public static BitString operator *(BitString current, BitString other) {
        return new BitString(current.Code ^ other.Code);
    }
}