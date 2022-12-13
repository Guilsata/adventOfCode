package utilsPuzzle5;

import java.util.ArrayList;
import java.util.Collections;

public class StackContainers{

    private ArrayList<String> containers;

    public String ToString(){
        String str = "";
        for(String s : containers){str=str+" "+s;}
        return str;
    }

    public String getTopContainers(){
        return containers.get(containers.size()-1);
    }

    public void stackListContainer(ArrayList<String> alc){
        containers.addAll(alc);
    }

    public ArrayList<String> popLastNContainer(int n){
        ArrayList<String> tmp = new ArrayList<>(containers.subList(containers.size()-n, containers.size()));
        containers.subList(containers.size()-n, containers.size()).clear();
        //Pour la première partie inversé la collection
        //Pour la deuxième ne pas le faire
        //Collections.reverse(tmp);
        return tmp;
    }

    public StackContainers(String[] data){
        this.containers = new ArrayList<>();
        for(String cd : data){
            this.containers.add(cd);
        }
    }
}