import java.util.*;
import java.io.*;

public class WordSearch {

	public static void main(String[] args) throws FileNotFoundException
	{
		//Instantiate inFile and outFile
		Scanner inFile = new Scanner(new FileReader("inData.txt"));
		PrintWriter outFile = new PrintWriter("outData.txt");
		
		//Instantiate length to first value in inFile
		int length = inFile.nextInt();
		
		/*Check to make sure length is between 1 and 100, end program if not
		 * This Error checking probably doesn't need to be in the MICS version*/
		if(length < 1 || length > 100){
			System.out.println("Length is out of range");
			inFile.close();	
		    outFile.close();
			return;
		}
		
		//Instantiate variables
		char [][] letterMatrix = new char[length][length];
		List<String> wordsToFind = new ArrayList<String>();
		String line = "";
		int lineCounter = 0;
		boolean wordExistsHorizontally = false;
		boolean wordExistsVertically = false;
		boolean wordExistsDiagonally = false;
		
		//read values into letterMatrix and wordsToFind
		while(inFile.hasNext()){
			
			line = inFile.next();
			
			//read characters into character matrix
			if(lineCounter != length){
			
				for(int lengthCounter = 0; lengthCounter < length; lengthCounter++){
					letterMatrix[lineCounter][lengthCounter] = line.charAt(lengthCounter);
				}
				lineCounter++;
			}
			//read words into array list of strings
			else if(!line.equals("0")){
				
				wordsToFind.add(line);
			}
			//when zero is read from the file
			else{
				
				break;
			}	
		}
		
		//Iterates through every element in the array list of words-to-be-found
		for (String word : wordsToFind) {
			wordExistsHorizontally = findHorizontal(letterMatrix, word, length, outFile);
			wordExistsVertically = findVertical(letterMatrix, word, length, outFile);
			wordExistsDiagonally = findDiagonal(letterMatrix, word, length, outFile);
			if(wordExistsHorizontally == false && wordExistsVertically == false
			   && wordExistsDiagonally == false){
				System.out.println(word + ": Not found");
				outFile.println(word + ": Not found");
			}
		}
		
		//close inFile and outFile
		inFile.close();	
	    outFile.close();
		
	}//end of main
	

	static boolean findHorizontal(char[][] letterMatrix, String word, int length, PrintWriter outFile){
		//declare variables
		int foundRow = 0;
		int foundColumn = 0;
		int currentIndex = 0;
		
		//loops through all characters in matrix until a word has been found to be left to right 
		for(int i = 0; i < length; i++){
			for(int z = 0; z < length; z++){
				/*if a character in the matrix matches the first character in the word, its
				 * position is recorded. Also checks if the character is far enough away
				 * from the right edge of the matrix to hold the word*/
				if(letterMatrix[i][z] == word.charAt(currentIndex) && length - z >= word.length()){
					foundRow = i;
					foundColumn = z;
					
					//iterates through the matrix row (left to right)
					for(int x = foundColumn; x <= foundColumn + word.length(); x++){
						//if the index iterator gets here, the word exists in the matrix
						if(currentIndex == word.length()){
							System.out.println(word + ": (" + (foundRow + 1) + "," +
							(foundColumn + 1) + ") to (" + (foundRow + 1) + "," + (x) + ")");
							outFile.println(word + ": (" + (foundRow + 1) + "," +
									(foundColumn + 1) + ") to (" + (foundRow + 1) + "," + (x) + ")");
							return true;
						}
						//if the matrix char and word char are equal, the index iterates
						else if(letterMatrix[foundRow][x] == word.charAt(currentIndex)){ 
								currentIndex++;
						}
						/*if a matrix char and word char are not equal, the index resets, and
						the next first-word char is checked*/
						else{
								currentIndex = 0;
								break;
							}
					}
				}
			}
		}			
		/*loops through all characters in matrix until a word has been found to be right to left
		 * only if the word has not been found to be left to right (AKA returned above)*/
		for(int i = 0; i < length; i++){
			for(int z = 0; z < length; z++){
				/*if a character in the matrix matches the first character in the word, its
				 * position is recorded. Also checks if the character is far enough away
				 * from the left edge of the matrix to hold the word*/
				if(letterMatrix[i][z] == word.charAt(currentIndex) && z >= word.length() - 1){
					foundRow = i;
					foundColumn = z;
					
					//iterates through the matrix row (right to left)
					for(int x = foundColumn; x >= foundColumn - word.length(); x--){
						//if the index iterator gets here, the word exists in the matrix
						if(currentIndex == word.length()){
							/*2 is added to x here for the output to compensate for the final 
							 *negative iteration and to display traditional coordinates (no zeros)*/
							System.out.println(word + ": (" + (foundRow + 1) + "," +
							(foundColumn + 1) + ") to (" + (foundRow + 1) + "," + (x + 2) + ")");
							outFile.println(word + ": (" + (foundRow + 1) + "," +
									(foundColumn + 1) + ") to (" + (foundRow + 1) + "," + (x + 2) + ")");
							return true;
						}
						//if the matrix char and word char are equal, the index iterates
						else if(letterMatrix[foundRow][x] == word.charAt(currentIndex)){ 
								currentIndex++;
						}
						/*if a matrix char and word char are not equal, the index resets, and
						the next first-word char is checked*/
						else{
								currentIndex = 0;
								break;
							}
					}
				}
			}
		}
		//if nothing is returned above, the word is not horizontal
		return false;
	}//end of findHorizontal
	
	
	static boolean findVertical(char[][] letterMatrix, String word, int length, PrintWriter outFile){
		//declare variables
		int foundColumn = 0;
		int foundRow = 0;
		int currentIndex = 0;
		
	//loops through all characters in matrix until a word has been found to be up to down 
		for(int i = 0; i < length; i++){
			for(int z = 0; z < length; z++){
				/*if a character in the matrix matches the first character in the word, its
				 * position is recorded. Also checks if the character is far enough away
				 * from the bottom edge of the matrix to hold the word*/
				if(letterMatrix[z][i] == word.charAt(currentIndex) && length - z >= word.length()){
					foundRow = z;
					foundColumn = i;
					
					//iterates through the matrix column (downwards)
					for(int x = foundRow; x <= foundRow + word.length(); x++){
						//if the index iterator gets here, the word exists in the matrix
						if(currentIndex == word.length()){
							System.out.println(word + ": (" + (foundRow + 1) + "," +
							(foundColumn + 1) + ") to (" + (x) + "," + (foundColumn + 1) + ")");
							outFile.println(word + ": (" + (foundRow + 1) + "," +
									(foundColumn + 1) + ") to (" + (x) + "," + (foundColumn + 1) + ")");
							return true;
						}
						//if the matrix char and word char are equal, the index iterates
						else if(letterMatrix[x][foundColumn] == word.charAt(currentIndex)){ 
								currentIndex++;
						}
						/*if a matrix char and word char are not equal, the index resets, and
						the next first-word char is checked*/
						else{
								currentIndex = 0;
								break;
							}
					}
				}
			}
		}			
		/*loops through all characters in matrix until a word has been found to be down to up
		 * only if the word has not been found to be up to down (AKA returned above)*/
		for(int i = 0; i < length; i++){
			for(int z = 0; z < length; z++){
				/*if a character in the matrix matches the first character in the word, its
				 * position is recorded. Also checks if the character is far enough away
				 * from the top edge of the matrix to hold the word*/
				if(letterMatrix[z][i] == word.charAt(currentIndex) && z >= word.length() - 1){
					foundRow = z;
					foundColumn = i;
					
					//iterates through the matrix column (upwards)
					for(int x = foundRow; x >= foundRow - word.length(); x--){
						//if the index iterator gets here, the word exists in the matrix
						if(currentIndex == word.length()){
							/*2 is added to x here for the output to compensate for the final 
							 *negative iteration and to display traditional coordinates (no zeros)*/
							System.out.println(word + ": (" + (foundRow + 1) + "," +
							(foundColumn + 1) + ") to (" + (x + 2) + "," + (foundColumn + 1) + ")");
							outFile.println(word + ": (" + (foundRow + 1) + "," +
									(foundColumn + 1) + ") to (" + (x + 2) + "," + (foundColumn + 1) + ")");
							return true;
						}
						//if the matrix char and word char are equal, the index iterates
						else if(letterMatrix[x][foundColumn] == word.charAt(currentIndex)){ 
								currentIndex++;
						}
						/*if a matrix char and word char are not equal, the index resets, and
						the next first-word char is checked*/
						else{
								currentIndex = 0;
								break;
							}
					}
				}
			}
		}
		//if nothing is returned above, the word is not vertical
		return false;
	}//end of findVertical
	
	
	static boolean findDiagonal(char[][] letterMatrix, String word, int length, PrintWriter outFile){
		//declare variables
		int foundRow = 0;
		int foundColumn = 0;
		int currentIndex = 0;
		
		//loops through all characters in matrix until a word has been found to be left to right downwards 
		for(int i = 0; i < length; i++){
			for(int z = 0; z < length; z++){
				/*if a character in the matrix matches the first character in the word, its
				 * position is recorded. Also checks if the character is far enough away
				 * from the right and bottom edges of the matrix to hold the word*/
				if(letterMatrix[i][z] == word.charAt(currentIndex) && length - z >= word.length() && length - i >= word.length()){
					foundRow = i;
					foundColumn = z;
					
					//iterates through the matrix (left to right downwards)
					for(int x = foundColumn, y = foundRow; x <= foundColumn + word.length(); x++, y++){
						//if the index iterator gets here, the word exists in the matrix
						if(currentIndex == word.length()){
							System.out.println(word + ": (" + (foundRow + 1) + "," +
							(foundColumn + 1) + ") to (" + (y) + "," + (x) + ")");
							outFile.println(word + ": (" + (foundRow + 1) + "," +
									(foundColumn + 1) + ") to (" + (y) + "," + (x) + ")");
							return true;
						}
						//if the matrix char and word char are equal, the index iterates
						else if(letterMatrix[y][x] == word.charAt(currentIndex)){ 
								currentIndex++;
						}
						/*if a matrix char and word char are not equal, the index resets, and
						the next first-word char is checked*/
						else{
								currentIndex = 0;
								break;
							}
					}
				}
			}
		}			
		/*loops through all characters in matrix until a word has been found to be right to left upwards
		 * only if the word has not been found to be left to right downwards (AKA returned above)*/
		for(int i = 0; i < length; i++){
			for(int z = 0; z < length; z++){
				/*if a character in the matrix matches the first character in the word, its
				 * position is recorded. Also checks if the character is far enough away
				 * from the left edge of the matrix to hold the word*/
				if(letterMatrix[i][z] == word.charAt(currentIndex) && z >= word.length() - 1 && i >= word.length() - 1){
					foundRow = i;
					foundColumn = z;
					
					//iterates through the matrix (Right to Left upwards)
					for(int x = foundColumn, y = foundRow; x >= foundColumn - word.length(); x--, y--){
						//if the index iterator gets here, the word exists in the matrix
						if(currentIndex == word.length()){
							/*2 is added to x here for the output to compensate for the final 
							 *negative iteration and to display traditional coordinates (no zeros)*/
							System.out.println(word + ": (" + (foundRow + 1) + "," +
							(foundColumn + 1) + ") to (" + (y + 2) + "," + (x + 2) + ")");
							outFile.println(word + ": (" + (foundRow + 1) + "," +
									(foundColumn + 1) + ") to (" + (y + 2) + "," + (x + 2) + ")");
							return true;
						}
						//if the matrix char and word char are equal, the index iterates
						else if(letterMatrix[y][x] == word.charAt(currentIndex)){ 
								currentIndex++;
						}
						/*if a matrix char and word char are not equal, the index resets, and
						the next first-word char is checked*/
						else{
								currentIndex = 0;
								break;
							}
					}
				}
			}
		}
		/*loops through all characters in matrix until a word has been found to be right to left downwards
		 * only if the word has not been found to be L to R downwards, or R to L upwards (AKA returned above)*/
		for(int i = 0; i < length; i++){
			for(int z = 0; z < length; z++){
				/*if a character in the matrix matches the first character in the word, its
				 * position is recorded. Also checks if the character is far enough away
				 * from the bottom edge of the matrix to hold the word*/
				if(letterMatrix[z][i] == word.charAt(currentIndex) && length - z >= word.length() && i >= word.length() - 1){
					foundRow = z;
					foundColumn = i;
					
					//iterates through the matrix column (Right to Left downwards)
					for(int x = foundRow, y = foundColumn; x <= foundRow + word.length(); x++, y--){
						//if the index iterator gets here, the word exists in the matrix
						if(currentIndex == word.length()){
							System.out.println(word + ": (" + (foundRow + 1) + "," +
							(foundColumn + 1) + ") to (" + (x) + "," + (y + 2) + ")");
							outFile.println(word + ": (" + (foundRow + 1) + "," +
									(foundColumn + 1) + ") to (" + (x) + "," + (y + 2) + ")");
							return true;
						}
						//if the matrix char and word char are equal, the index iterates
						else if(letterMatrix[x][y] == word.charAt(currentIndex)){ 
								currentIndex++;
						}
						/*if a matrix char and word char are not equal, the index resets, and
						the next first-word char is checked*/
						else{
								currentIndex = 0;
								break;
							}
					}
				}
			}
		}			
		/*loops through all characters in matrix until a word has been found to be right to left downwards
		 * only if the word has not been found to be L to R downwards, or R to L upwards, or R to L downwards
		 *  (AKA returned above)*/
		for(int i = 0; i < length; i++){
			for(int z = 0; z < length; z++){
				/*if a character in the matrix matches the first character in the word, its
				 * position is recorded. Also checks if the character is far enough away
				 * from the top edge of the matrix to hold the word*/
				if(letterMatrix[z][i] == word.charAt(currentIndex) && z >= word.length() - 1 && length - i >= word.length()){
					foundRow = z;
					foundColumn = i;
					
					//iterates through the matrix column (Left to Right upwards)
					for(int x = foundRow, y = foundColumn; x >= foundRow - word.length(); x--, y++){
						//if the index iterator gets here, the word exists in the matrix
						if(currentIndex == word.length()){
							/*2 is added to x here for the output to compensate for the final 
							 *negative iteration and to display traditional coordinates (no zeros)*/
							System.out.println(word + ": (" + (foundRow + 1) + "," +
							(foundColumn + 1) + ") to (" + (x + 2) + "," + (y) + ")");
							outFile.println(word + ": (" + (foundRow + 1) + "," +
									(foundColumn + 1) + ") to (" + (x + 2) + "," + (y) + ")");
							return true;
						}
						//if the matrix char and word char are equal, the index iterates
						else if(letterMatrix[x][y] == word.charAt(currentIndex)){ 
								currentIndex++;
						}
						/*if a matrix char and word char are not equal, the index resets, and
						the next first-word char is checked*/
						else{
								currentIndex = 0;
								break;
							}
					}
				}
			}
		}
		//if nothing is returned above, the word is not diagonal
		return false;
	}//end of findDiagonal
	
	
}//end of class