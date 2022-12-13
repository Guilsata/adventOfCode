import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

public class UtilsFile{
    public static ArrayList<String> myReadFile(String filename){
      String line;
      String pathname = "input\\"+filename;
      ArrayList<String> data = new ArrayList<String>();
      try{
        // Le fichier d'entrée
        File file = new File(pathname);    
        // Créer l'objet File Reader
        FileReader fr = new FileReader(file); 
        BufferedReader br = new BufferedReader(fr);
      
        while((line = br.readLine()) != null){
          data.add(line);
        }
        fr.close();
      }
      catch(IOException e)
      {
        e.printStackTrace();
      }
    return data;
  }
}
