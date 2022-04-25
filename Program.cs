using System;
using System.Collections.Generic;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Console_Test_HackerRank
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {

                string delimiter = ",";
                string routeFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MOCK_DATA.csv"); //ROUTE OF THE NEW FILE CSV
                var reader = new StreamReader(File.OpenRead(routeFile));//READ THE DOCUMENT 

                string[] data = new string[10];//CRAETE AN ARRAY TO SAVE EACH ITEM 
                List<string[]> listData = new List<string[]>(); //CREATE A LIST OF ARRAYS TO SAVE ALL DATA 

                while (!reader.EndOfStream) //UNTIL FINISH
                {
                    var row = reader.ReadLine(); //READ EACH LINE
                    string[] rowItem = row.Split(delimiter);//SPLIT THE LINE TO REMOVE THE DELIMITER

                    int cont = 1; //EACH ITEM HAS 5 ITEMS (PERSONS) SO WE START FROM THE FIRST ALWAYS GOING TO HAVE DATA
                    int contIni = 5; //DATA OF EVERY ROW START AT THE FIFTH POSITION wITH THE NAME

                    for (int i = 0; i < rowItem.Length; i++)
                    {
                        data = new string[10];//INTANCE THE ARRAY TO SAVE NEW DATA EVERY TIME ITERATES IN THE LOOP
                        cont = cont + 1;//INCREMENT THE COUNTER TO FOLLOW WITH THE SECOND,THIRD,FOURTH NAME
                        i = contIni;//SET THE ITERARATOR VALUE FROM THE BEGIN OF THE DATA (NAME)

                        //SET THE FIRST FIVE VALUES OF THE ARRAY
                        data[0] = rowItem[0];
                        data[1] = rowItem[1];
                        data[2] = rowItem[2];
                        data[3] = rowItem[3];
                        data[4] = rowItem[4];

                        //VERIFY IF YHE FIRST VALUE OF THE ARRAY CONTAINS THE WORD "FLAG" JUST TO RECOGNIZE THATS FOR THE HEADERS OF THE CSV
                        if (data[0].ToString().Contains("Flag1"))
                        {
                            //SET THE NEXT FIVE VALUES OF THE ARRAY  FOR THE HEADER
                            data[5] = "Name";
                            data[6] = "Adress";
                            data[7] = "City";
                            data[8] = "State";
                            data[9] = "Zip";
                        }
                        else
                        {
                            //INDEX STARTS IN 5 POSITION FOR THE NAME,
                            //THEN EVERY 4 VALUES STARTS ADRESS AND SO ON
                            //THE KEY VALUE IS 4 SO INCREMENT THE INDEX
                            //TO MOVE TO THE RESPECTIVE DATA
                            data[5] = rowItem[i];
                            data[6] = rowItem[i + 4];
                            data[7] = rowItem[i + 8];
                            data[8] = rowItem[i + 12];
                            data[9] = rowItem[i + 16];
                        }

                        //VERIFY IF THE ITEM HAVE DATA TO INCLUDE IN
                        if (!string.IsNullOrEmpty(data[5].ToString()) && !string.IsNullOrEmpty(data[6].ToString()) &&
                            !string.IsNullOrEmpty(data[7].ToString()) && !string.IsNullOrEmpty(data[8].ToString()) && !string.IsNullOrEmpty(data[9].ToString()))
                        {
                            listData.Add(data);
                        }

                        //VERIFY IF THE ROW IS FOR THE HEADERS TO SKIP TO THE NEXT ROW
                        if (data[0].ToString().Contains("Flag1"))
                            i = 26;
                        else
                            i = cont * 5 + 1;//SET THE INDEX FOR THE NEXT PERSON IN THE LINE

                        contIni = contIni + 1;
                    }
                }

                string route = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "narrowFormat.csv"); //ROUTE OF THE NEW FILE CSV

                if (File.Exists(route))//VERIFY IF EXIST 
                    File.Delete(route);//DELETE THE FILE

                using (StreamWriter doc = new StreamWriter(route)) //USING STREAMWRITER TO CREATE THE FILE
                {
                    foreach (var item in listData)
                    {
                        doc.Write(item[0] + delimiter + item[1] + delimiter + item[2] + delimiter + item[3] +
                            delimiter + item[4] + delimiter + item[5] + delimiter + item[6] + delimiter + item[7] +
                            delimiter + item[8] + delimiter + item[9] + Environment.NewLine); //CONCAT ALL VALUES PER LINE
                    }
                }

                Console.WriteLine("File Directory: " + route);//MESSAGE
                Console.ReadLine();//WAIT TO SEE THE MESSAGE
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occours : " + ex);//MESSAGE
            }
        }
    }
}
