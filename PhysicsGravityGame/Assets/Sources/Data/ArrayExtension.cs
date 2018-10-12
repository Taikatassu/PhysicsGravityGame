using System;

public static class ArrayExtension {
    public static bool Contains<T>(this T[] array, T item) {

        for(int i = 0; i < array.Length; i++) {
            if(array[i].Equals(item)) {
                return true;
            }
        }

        return false;
    }
}
