using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using System.IO;

public class RoomStatePacket
{
    public int packetID { get; set; }

    public RoomStatePacket(int dummyPacketID = 0)
    {
        packetID = dummyPacketID;
    }

    public static RoomStatePacket Deseralize(byte[] data)
    {
        var result = new RoomStatePacket();
        using (MemoryStream m = new MemoryStream(data)) {
            using (BinaryReader reader = new BinaryReader(m)) {
                result.packetID = reader.ReadInt32();
                //TODO: read the rest of the message

            }
        }
        return result;
    }

    public byte[] Serialize()
    {
        using (MemoryStream m = new MemoryStream()) {
            using (BinaryWriter writer = new BinaryWriter(m)) {
                writer.Write(packetID);

                //TODO: write the rest of it.

                writer.Write('\0');
            }
            return m.ToArray();
        }
    }
}

public class MultiplayerState : MonoBehaviour
{
    int latestPacketID = 0; //TODO: will be serialized and incremented

    void Start()
    {
    }

    void printOutputLine(string msg)
    {
        Debug.Log(msg);
    }

    void Update()
    {
        Packet incomingPacket = Net.ReadPacket();
        while (incomingPacket != null) {
            byte[] rawBits = new byte[incomingPacket.Size];
            incomingPacket.ReadBytes(rawBits);

            RoomStatePacket newRoomState = RoomStatePacket.Deseralize(rawBits);

            printOutputLine("Received Packet from UserID: " + incomingPacket.SenderID.ToString());
            
            //Oculus default packets: 365645825
            printOutputLine("Received Packet ID: " + newRoomState.packetID.ToString() + " Length: " + incomingPacket.Size);

            incomingPacket = Net.ReadPacket();
        }

        //TODO only send if you're the server.
        //Net.SendPacketToCurrentRoom(new RoomStatePacket(43).Serialize(), SendPolicy.Unreliable);
    }
}
