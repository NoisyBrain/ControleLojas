using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace code.classesAux
{
    public class Salva
    {

        public string ConverteObjectToJson(object obj)
        {
            string jsonObject = JsonSerializer.Serialize<object>(obj);
            return jsonObject;
        }

        public string InfoSaver(string path, object obj, string nomeArquivo)
        {
            string jsonObject = ConverteObjectToJson(obj);

            string completePath = $"{path}/{nomeArquivo}";

            File.WriteAllText(completePath, jsonObject);

            return completePath;
        }

        public string DirectoryCreater(string path, string nomeDiretorio)
        {
            string completePath = $"{path}/{nomeDiretorio}";

            DirectoryInfo di = Directory.CreateDirectory(completePath);

            return completePath;
        }

        public bool VerificarExistencia(string path)
        {
            FileInfo di = new FileInfo(path);
            if(di.Exists)
            {
                return true;
            }else
            {
                return false;
            }
        }
    }
}