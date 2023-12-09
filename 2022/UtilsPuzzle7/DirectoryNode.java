package utilsPuzzle7;

import java.util.ArrayList;

public class DirectoryNode {
    private final String name;
    private final int id;
    private final DirectoryNode parent;

    private static int countInstance = 0;

    private record MyFile(String name, int size){};
    
    public static DirectoryNode initSeed(){
        DirectoryNode dn = new DirectoryNode("/",null);
        return dn;
    }

    private static int getId() {
        return countInstance;
    }

    private static void incrementId() {
        countInstance++;
    }

    private ArrayList<DirectoryNode> children;
    private ArrayList<MyFile> leaf;

    private DirectoryNode(String name, DirectoryNode parent) {
        this.name = name;
        this.parent = parent;
        this.id = DirectoryNode.getId();
        DirectoryNode.incrementId();
    }

    private boolean alReadyExist(String name){
        for(DirectoryNode dn : children){
            if(dn.itIsMyName(name)){return true;}
        }
        return false;
    }

    private boolean fileAlReadyExist(String name){
        for(MyFile mf : leaf){
            if(mf.name==name){return true;}
        }
        return false;
    }

    private boolean itIsMyName(String name){
        return this.name == name;
    }

    public void addChildren(String name) {
        if(!alReadyExist(name)){
            children.add(new DirectoryNode(name, this));
        }
    }

    public void addFile(String name, int size){
        if(!fileAlReadyExist(name)){
            leaf.add(new MyFile(name, size));
        }
    }

    public DirectoryNode getParent(){return this.parent;} 

    public int getSize(){
        return //faire une lambda ici
    }
}
