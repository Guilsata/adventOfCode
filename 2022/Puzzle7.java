import java.util.ArrayList;

import utilsPuzzle7.DirectoryNode;

public class Puzzle7 {
    
    // if first car $ is command
    // cd [..] go in [..]
    // cd .. go to parent directory
    // ls see what's in [..]
    // dir [.] is a directory named [.]
    // 999999 [...] is a file named [...] of size 99999
    /*
     * Usable struc :
     * tree with each node is a directory and each leaf is a file
     * a directory has one name, children (either directory or file) and one parent
     * there is one class directory with method to calculate his size and the size
     * of her children
     * only the root node have no parent
     * 
     * it could be easier to have a dictionnary which link name or an unique id and
     * the directory object
     */
    public static void main(String[] args){
        ArrayList<String> file = UtilsFile.myReadFile("data7.txt");
        DirectoryNode fileSystem = DirectoryNode.initSeed();
        DirectoryNode currentDirectory;
        for(String line : file){

        }
    }
}
