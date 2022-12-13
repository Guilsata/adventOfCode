package UtilsPuzzle7;

import java.util.ArrayList;

public class DirectoryNode {
    private final String name;
    private final int id;
    private final DirectoryNode parent;

    private static int countInstance = 0;

    private static int getId() {
        return countInstance;
    }

    private static void incrementId() {
        countInstance++;
    }

    private ArrayList<DirectoryNode> children;
    private ArrayList<Integer> leaf;

    public DirectoryNode(String name, DirectoryNode parent) {
        this.name = name;
        this.parent = parent;
        this.id = DirectoryNode.getId();
        DirectoryNode.incrementId();
    }

    public void addChildren(String name) {
        // todo
    }
}
