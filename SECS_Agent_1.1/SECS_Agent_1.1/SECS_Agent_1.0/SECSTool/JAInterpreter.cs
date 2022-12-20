using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace SECSTool
{
    public class JAInterpreter
    {
        public JObject Array2JSONObj(int s,int f, ArrayList arrayList)
        {
            JObject jObj=new JObject(new JProperty("SxFx","S"+s.ToString()+"F"+f.ToString()));
            JArray jArray=new JArray();
            jArray.Add(ArrayList2JSON(arrayList, new JArray()));
            jObj.Add("SecsMsg", jArray);
            return jObj;
        }

        public JObject Data2JSONObj(int s,int f,dynamic data)
        {
            try
            {
                JObject jObj = new JObject(new JProperty("SxFx", "S" + s.ToString() + "F" + f.ToString()));
                if(data!=null)
                {
                    ArrayList tmpArrayList=new ArrayList() { data};
                    jObj.Add("SecsMsg",ArrayList2JSON(tmpArrayList,new JArray()));
                }
                return jObj;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private JArray ArrayList2JSON(ArrayList arrList,JArray listArray)
        {
            foreach(var item in arrList)
            {
                JObject tmpObj = null;
                switch(Type.GetTypeCode(item.GetType()))
                {
                    case TypeCode.Byte:
                        tmpObj = new JObject(ListData2JSONProperty(SECSType.U1, item));
                        break;
                    case TypeCode.SByte:
                        tmpObj = new JObject(ListData2JSONProperty(SECSType.I1, item));
                        break;
                    case TypeCode.Boolean:
                        tmpObj = new JObject(ListData2JSONProperty(SECSType.BOOLEAN, item));
                        break;
                    case TypeCode.Int16:
                        tmpObj = new JObject(ListData2JSONProperty(SECSType.I2, item));
                        break;
                    case TypeCode.Int32:
                        tmpObj = new JObject(ListData2JSONProperty(SECSType.U4, item));
                        break;
                    case TypeCode.Int64:
                        tmpObj = new JObject(ListData2JSONProperty(SECSType.I8, item));
                        break;
                        break;
                    case TypeCode.UInt16:
                        tmpObj = new JObject(ListData2JSONProperty(SECSType.U2, item));
                        break;
                    case TypeCode.UInt32:
                        tmpObj = new JObject(ListData2JSONProperty(SECSType.U4, item));
                        break;
                    case TypeCode.UInt64:
                        tmpObj = new JObject(ListData2JSONProperty(SECSType.U8, item));
                        break;
                    case TypeCode.Single:
                        tmpObj = new JObject(ListData2JSONProperty(SECSType.F4, item));
                        break;
                    case TypeCode.Double:
                        tmpObj = new JObject(ListData2JSONProperty(SECSType.F8, item));
                        break;
                    case TypeCode.String:
                            tmpObj = new JObject(ListData2JSONProperty(SECSType.A, item));
                        break;
                    case TypeCode.Object:
                        JArray tmpArray=new JArray();
                        listArray.Add(ArrayList2JSON((ArrayList)item, tmpArray));
                        break;
                    default:
                        throw new Exception("Receieve Error Type!!");
                }

                if(tmpObj!=null)
                    listArray.Add(tmpObj);
            }
            return listArray;
        }

        private JProperty ListData2JSONProperty(SECSType sType,dynamic data)
        {
            return new JProperty(DataConvert(sType.ToString(), data, false), sType.ToString());
        }

        private dynamic DataConvert(string dataType,dynamic data,bool toData) //toData: Ture:JSON->ArrayList, False:ArrayList->JSON
        {
            switch((SECSType)Enum.Parse(typeof(SECSType),dataType))
            {
                case SECSType.B:
                    if(toData)
                    {
                        StringBuilder sb=new StringBuilder();
                        foreach(char c in data.ToCharArray())
                        {
                            sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
                        }
                        return sb.ToString();
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        char[] hexChar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
                        int high = ((data & 0x00) >> 4);
                        int low = ((data & 0x0f));
                        sb.Append("0x");
                        sb.Append(hexChar[high]);
                        sb.Append(hexChar[low]);
                        return sb.ToString();
                    }
                
                case SECSType.BOOLEAN:
                    return (toData) ? bool.Parse(data) : Convert.ToString(data);

                case SECSType.A:
                    return data;

                case SECSType.J:
                    break;

                case SECSType.I1:
                    return (toData) ? byte.Parse(data) : Convert.ToString(data);

                case SECSType.I2:
                    return (toData) ? short.Parse(data) : Convert.ToString(data);

                case SECSType.I4:
                    return (toData) ? int.Parse(data) : Convert.ToString(data);

                case SECSType.I8:
                    return (toData) ? long.Parse(data) : Convert.ToString(data);

                case SECSType.F4:
                    return (toData) ? float.Parse(data) : Convert.ToString(data);

                case SECSType.F8:
                    return (toData) ? double.Parse(data) : Convert.ToString(data);

                case SECSType.U1:
                    return (toData) ? byte.Parse(data) : Convert.ToString(data);

                case SECSType.U2:
                    return (toData) ? ushort.Parse(data) : Convert.ToString(data);

                case SECSType.U4:
                    return (toData) ? uint.Parse(data) : Convert.ToString(data);

                case SECSType.U8:
                    return (toData) ?ulong.Parse(data) : Convert.ToString(data);
                    break;
            }
            return null;
        }

        public ArrayList JSONObj2Array(string JString)
        {
            try
            {
                var jObj = JsonConvert.DeserializeObject<dynamic>(JString);
                ArrayList tmpArrayList = new ArrayList();
                tmpArrayList.Add(jObj["SxFx"]);
                tmpArrayList.Add(JSON2ArrayList(jObj["LIST"], new ArrayList()));
                return tmpArrayList;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private ArrayList JSON2ArrayList(JArray items,ArrayList arrList)
        {
            if(items==null) throw new ArgumentNullException("Receieve null object jToken");
            try
            {
                foreach (var item in items)
                {
                    switch (item.Type)
                    {
                        case JTokenType.Object:
                            foreach (var element in item)
                            {
                                arrList.Add(DataConvert(((JProperty)(element)).Value.ToString(), ((JProperty)(element)).Name, true));
                            }
                            break;
                        case JTokenType.Array:
                            ArrayList tmpList = new ArrayList();
                            arrList.Add(JSON2ArrayList(item as JArray, tmpList));
                            break;
                        default:
                            throw new Exception("Receieve Error Type!!");
                    }
                }
                return arrList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
