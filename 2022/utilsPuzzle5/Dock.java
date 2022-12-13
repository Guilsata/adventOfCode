package utilsPuzzle5;

import java.util.ArrayList;

public class Dock{

    private ArrayList<StackContainers> listOfStackOfContainers; 

    private record Command(int numberOfContainer, int from, int to) { public String ToString(){return this.numberOfContainer +":"+this.from+":"+this.to;}}

    private Command readLine(String line){
        //move NUMBER from NUMBER to NUMBER
        String[]tmp = line.split(" ");
        Command com = new Command(Integer.parseInt(tmp[1]), Integer.parseInt(tmp[3]),Integer.parseInt(tmp[5]));
        return com;
    }

    public String ToString(){
        int i = 0;
        String str="";
        for(StackContainers sc : listOfStackOfContainers){
            i++;
            str = str +i+" : "+sc.ToString()+"\n";
        }
        return str;
    }

    public void printAllTopContainers(){
        for(StackContainers sc : listOfStackOfContainers){
            System.out.print(sc.getTopContainers());
        }
        System.out.println("");
    }

    public void executeCommand(String line){
        Command command = readLine(line);
        System.out.println(line);
        System.out.println(command);
        listOfStackOfContainers.get(command.to-1).stackListContainer(listOfStackOfContainers.get(command.from-1).popLastNContainer(command.numberOfContainer));
    }

    public Dock(){
        this.listOfStackOfContainers = new ArrayList<>();
        String[] t1 = {"B","Q","C"};
        this.listOfStackOfContainers.add(new StackContainers(t1));
        String[] t2 = {"R","Q","W","Z"};
        this.listOfStackOfContainers.add(new StackContainers(t2));
        String[] t3 = {"B","M","R","L","V"};
        this.listOfStackOfContainers.add(new StackContainers(t3));
        String[] t4 = {"C","Z","H","V","T","W"};
        this.listOfStackOfContainers.add(new StackContainers(t4));
        String[] t5 = {"D","Z","H","B","N","V","G"};
        this.listOfStackOfContainers.add(new StackContainers(t5));
        String[] t6 = {"H","N","P","C","J","F","V","Q"};
        this.listOfStackOfContainers.add(new StackContainers(t6));
        String[] t7 = {"D","G","T","R","W","Z","S"};
        this.listOfStackOfContainers.add(new StackContainers(t7));
        String[] t8 = {"C","G","M","N","B","W","Z","P"};
        this.listOfStackOfContainers.add(new StackContainers(t8));
        String[] t9 = {"N","J","B","M","W","Q","F","P"};
        this.listOfStackOfContainers.add(new StackContainers(t9));
    }
}
