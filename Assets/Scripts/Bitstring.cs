using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BitString {
    // Define a bitstring class just for easy hashing, with operator * that is just a xor.
    public int code { get; private set; }
    public BitString(bool random){
        if(random){
            code = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        }else{
            code = 0;
        }
    }

    public BitString(int code){
        this.code = code;
    }

    public override string ToString(){
        string binary = Convert.ToString(code, 2);
        return binary;
    }  

    public static BitString operator *(BitString current, BitString other){
        return new BitString(current.code ^ other.code);
    }
}