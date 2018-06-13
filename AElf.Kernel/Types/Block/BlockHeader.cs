﻿using AElf.Kernel.Extensions;
using System;
using AElf.Cryptography.ECDSA;
using Google.Protobuf;
using Org.BouncyCastle.Math;

// ReSharper disable once CheckNamespace
namespace AElf.Kernel
{
    public partial class BlockHeader : IBlockHeader
    {
        public BlockHeader(Hash preBlockHash)
        {
            PreviousBlockHash = preBlockHash;
        }

        public Hash GetHash()
        {
            return GetSignatureData();
        }

        public byte[] Serialize()
        {
            return this.ToByteArray();
        }
        
        public ECSignature GetSignature()
        {
            BigInteger[] sig = new BigInteger[2];
            sig[0] = new BigInteger(R.ToByteArray());
            sig[1] = new BigInteger(S.ToByteArray());
            
            return new ECSignature(sig);
        }
        
        public byte[] GetSignatureData()
        {
            var rawBlock = new BlockHeader
            {
                ChainId = ChainId.Clone(),
                Index = Index,
                PreviousBlockHash = PreviousBlockHash.Clone(),
                MerkleTreeRootOfTransactions = MerkleTreeRootOfTransactions.Clone(),
                MerkleTreeRootOfWorldState = MerkleTreeRootOfWorldState.Clone()
            };
            
            return rawBlock.ToByteArray();
        }
    }
}