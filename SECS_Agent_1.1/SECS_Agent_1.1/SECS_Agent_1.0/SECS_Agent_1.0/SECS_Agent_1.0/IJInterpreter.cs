﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SECSTool;
using Secs4Net;
using Newtonsoft.Json.Linq;

namespace SECS_Agent_1._0
{
    public class IJInterpreter
    {
        /// <summary>
        /// Convert SECS JArray to Item
        /// </summary>
        /// <param name="jArr"></param>
        /// <param name="items"></param>
        /// <returns>Item object</returns>
        public Item JArray2Item(JArray jArr, Item[] items)
        {
            try
            {
                if(jArr == null||jArr.Count==0)
                    throw new ArgumentNullException(nameof(jArr));
                Item tmpItem = null;
                foreach (var element in jArr)
                {
                    switch (element.Type)
                    {
                        case JTokenType.Object:
                                tmpItem = Item.L(items.Append(JProp2ItemValue((JObject)element)));
                                items = tmpItem.Items;
                            break;
                        case JTokenType.Array:
                            tmpItem = Item.L(items.Append(JArray2Item(element as JArray, Item.L().Items)));
                            items = tmpItem.Items;
                            break;
                    }
                }
                return Item.L(items);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public Item JArray2Item(JArray jArr)
        //{
        //    try
        //    {
        //        Item tmpItem = null;
        //        Item resultItem = null;
        //        foreach (var element in jArr)
        //        {
        //            switch (element.Type)
        //            {
        //                case JTokenType.Object:
        //                    foreach (var prop in element)
        //                    {
        //                        tmpItem = Item.L(JProp2ItemValue(((JProperty)(prop)).Value.ToString(), ((JProperty)(prop)).Name));
        //                        if (resultItem == null)
        //                            resultItem = Item.L(tmpItem.Items);
        //                        else
        //                            resultItem = Item.L(resultItem.Items.Append(tmpItem));
        //                    }
        //                    break;
        //                case JTokenType.Array:
        //                    if (resultItem == null)
        //                    {
        //                        tmpItem = Item.L(JArray2Item(element as JArray));
        //                        resultItem = Item.L(tmpItem.Items);
        //                    }
        //                    else
        //                    {
        //                        tmpItem = Item.L(resultItem.Items.Append(JArray2Item(element as JArray)));
        //                        resultItem = Item.L(resultItem.Items.Append(tmpItem));
        //                    }
        //                    break;
        //            }
        //        }
        //        return Item.L(resultItem.Items);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        /// <summary>
        /// Convert SECS Item to JArray
        /// </summary>
        /// <param name="item"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        /// 

        public JObject Item2Prop(Item item)
        {
            try
            {
                JObject tmpObj;
                string itemTypeStr = item.Format.ToString();
                switch (item.Format)
                {
                    case SecsFormat.Boolean:
                        tmpObj = new JObject(new JProperty(itemTypeStr, ItemValue2String(item.GetMemory<bool>())));
                        break;
                    case SecsFormat.U1:
                        tmpObj = new JObject(new JProperty(itemTypeStr, ItemValue2String(item.GetMemory<byte>())));
                        break;
                    case SecsFormat.U4:
                        tmpObj = new JObject(new JProperty(itemTypeStr, ItemValue2String(item.GetMemory<uint>())));
                        break;
                    case SecsFormat.U8:
                        tmpObj = new JObject(new JProperty(itemTypeStr, ItemValue2String(item.GetMemory<ulong>())));
                        break;
                    case SecsFormat.F4:
                        tmpObj = new JObject(new JProperty(itemTypeStr, ItemValue2String(item.GetMemory<float>())));
                        break;
                    case SecsFormat.F8:
                        tmpObj = new JObject(new JProperty(itemTypeStr, ItemValue2String(item.GetMemory<double>())));
                        break;
                    case SecsFormat.Binary:
                        tmpObj = new JObject(new JProperty("B", ItemValue2String(item.GetMemory<byte>()))); //Due to Byte is "B" in SECS format. 
                        break;
                    case SecsFormat.ASCII:
                        tmpObj = new JObject(new JProperty("A", item.GetString()));
                        break;
                    default:
                        tmpObj = null;
                        break;
                }
                return tmpObj;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public JArray Item2JArray(Item item, JArray jArray)
        {
            try
            {
                foreach (Item element in item.Items)
                {
                    JObject tmpObj = null;
                    string itemTypeStr = element.Format.ToString();
                    switch (element.Format)
                    {
                        case SecsFormat.Boolean:
                            tmpObj = new JObject(new JProperty(itemTypeStr, ItemValue2String(element.GetMemory<bool>())));
                            break;
                        case SecsFormat.U1:
                            tmpObj = new JObject(new JProperty(itemTypeStr, ItemValue2String(element.GetMemory<byte>())));
                            break;
                        case SecsFormat.U4:
                            tmpObj = new JObject(new JProperty(itemTypeStr, ItemValue2String(element.GetMemory<uint>())));
                            break;
                        case SecsFormat.U8:
                            tmpObj = new JObject(new JProperty(itemTypeStr, ItemValue2String(element.GetMemory<ulong>())));
                            break;
                        case SecsFormat.F4:
                            tmpObj = new JObject(new JProperty(itemTypeStr, ItemValue2String(element.GetMemory<float>())));
                            break;
                        case SecsFormat.F8:
                            tmpObj = new JObject(new JProperty(itemTypeStr, ItemValue2String(element.GetMemory<double>())));
                            break;
                        case SecsFormat.Binary:
                            tmpObj = new JObject(new JProperty("B", ItemValue2String(element.GetMemory<byte>()))); //Due to Byte is "B" in SECS format. 
                            break;
                        case SecsFormat.ASCII:
                            tmpObj = new JObject(new JProperty("A", element.GetString()));
                            break;
                        case SecsFormat.List:
                            JArray tmpArray = new JArray();
                            jArray.Add(Item2JArray(element, tmpArray));
                            break;
                    }
                    if (tmpObj != null)
                        jArray.Add(tmpObj);
                }
                return jArray;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Support to concatebate string of value while itemValue is multi-value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public string ItemValue2String<T>(Memory<T> buffer)
        {
            try
            {
                string str = String.Empty;
                dynamic obj = buffer;
                if (typeof(T) == typeof(Byte))
                {
                    byte[] array = obj.ToArray();
                    str = BitConverter.ToString(array).Replace("-", " 0x");
                    str = "0x" + str;
                }
                else
                {
                    T[] array = obj.ToArray();
                    foreach (var value in array)
                    {
                        str = value.ToString();
                        str = " " + str;
                    }
                    str = str.Remove(0, 1);
                }
                return str;
            }
            catch(Exception es)
            {
                return null;
            }
        }


        /// <summary>
        /// Support JProperty to item value, need provide property key&value as arguments
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <returns>Item</returns>
        /// <exception cref="Exception"></exception>
        public Item JProp2ItemValue(JObject jObj)
        {
            try
            {
                string dataType = ((JProperty)(jObj.First)).Name;
                string data = ((JProperty)(jObj.First)).Value.ToString();
                switch ((SECSType)Enum.Parse(typeof(SECSType), dataType))
                {
                    case SECSType.B:
                        return Item.B(Convert.ToByte(data,16));

                    case SECSType.BOOLEAN:
                        return Item.Boolean(Boolean.Parse(data));

                    case SECSType.A:
                        return Item.A(data);

                    case SECSType.J:
                        break;

                    case SECSType.I1:
                        return Item.I1(sbyte.Parse(data));

                    case SECSType.I2:
                        return Item.I2(short.Parse(data));

                    case SECSType.I4:
                        return Item.I4(int.Parse(data));

                    case SECSType.I8:
                        return Item.I8(long.Parse(data));

                    case SECSType.F4:
                        return Item.F4(float.Parse(data));

                    case SECSType.F8:
                        return Item.F8(double.Parse(data));

                    case SECSType.U1:
                        return Item.U1(byte.Parse(data));

                    case SECSType.U2:
                        return Item.U2(ushort.Parse(data));

                    case SECSType.U4:
                        return Item.U4(uint.Parse(data));

                    case SECSType.U8:
                        return Item.U8(ulong.Parse(data));
                        break;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

        }

    }
}
