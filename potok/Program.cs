using System;

namespace potok
{
    internal class Program
    {
        static void Main()
        {
            SummaryRanges obj = new SummaryRanges();
            obj.AddNum(1);  
            PrintIntervals(obj.GetIntervals());  
            obj.AddNum(3);  
            PrintIntervals(obj.GetIntervals());  
            obj.AddNum(7);  
            PrintIntervals(obj.GetIntervals());  
            obj.AddNum(2); 
            PrintIntervals(obj.GetIntervals());  
            obj.AddNum(6);  
            PrintIntervals(obj.GetIntervals());  
        }

        // Метод для вывода интервалов 
        private static void PrintIntervals(int[][] intervals)
        {
            Console.Write("[");
            for (int i = 0; i < intervals.Length; i++)
            {
                Console.Write($"[{intervals[i][0]}, {intervals[i][1]}]");
                if (i < intervals.Length - 1) Console.Write(", ");
            }
            Console.WriteLine("]");
        }

        // Класс для обработки потока данных и формирования непересекающихся интервалов
        public class SummaryRanges
        {
            // Массив для хранения интервалов 
            private int[,] intervals;
            private int size; 

            public SummaryRanges()
            {
                // Инициализируем массив для хранения максимум 100 интервалов
                intervals = new int[100, 2];
                size = 0; // Начальный размер массива интервалов
            }

            // Метод для добавления нового числа в потоке
            public void AddNum(int value)
            {
                // Создаем новый интервал для добавляемого числа
                int[] newInterval = { value, value };
                // Массив для хранения новых интервалов после добавления числа
                int[,] newIntervals = new int[100, 2];
                int newSize = 0; 
                bool placed = false; // Флаг для отслеживания, добавлен ли новый интервал

                // Обрабатываем каждый существующий интервал и проверяем его взаимодействие с новым числом
                for (int i = 0; i < size; i++)
                {
                    // Извлекаем текущий интервал
                    int[] currentInterval = { intervals[i, 0], intervals[i, 1] };

                    // Проверка: если число уже входит в существующий интервал, добавление не требуется
                    if (currentInterval[0] <= value && value <= currentInterval[1])
                    {
                        return;
                    }

                    // Если новый интервал находится перед текущим и не пересекается с ним
                    if (currentInterval[0] > newInterval[1] + 1)
                    {
                        // Добавляем новый интервал в новый массив, если он еще не добавлен
                        if (!placed)
                        {
                            newIntervals[newSize, 0] = newInterval[0];
                            newIntervals[newSize, 1] = newInterval[1];
                            newSize++;
                            placed = true; 
                        }
                        // Добавляем текущий интервал в новый массив
                        newIntervals[newSize, 0] = currentInterval[0];
                        newIntervals[newSize, 1] = currentInterval[1];
                        newSize++;
                    }
                    // Если новый интервал пересекается с текущим
                    else if (currentInterval[1] + 1 >= newInterval[0] && currentInterval[0] - 1 <= newInterval[1])
                    {
                        // Объединяем текущий и новый интервалы
                        newInterval[0] = Math.Min(newInterval[0], currentInterval[0]);
                        newInterval[1] = Math.Max(newInterval[1], currentInterval[1]);
                    }
                    // Если текущий интервал не пересекается с новым, добавляем его как есть
                    else
                    {
                        newIntervals[newSize, 0] = currentInterval[0];
                        newIntervals[newSize, 1] = currentInterval[1];
                        newSize++;
                    }
                }

                // Если новый интервал ещё не добавлен, добавляем его в конец
                if (!placed)
                {
                    newIntervals[newSize, 0] = newInterval[0];
                    newIntervals[newSize, 1] = newInterval[1];
                    newSize++;
                }

                // Обновляем основной массив интервалов и его размер после добавления нового числа
                intervals = newIntervals;
                size = newSize;
            }

            // Метод для получения текущего списка непересекающихся интервалов
            public int[][] GetIntervals()
            {
                // Создаем результативный массив интервалов, используя текущий размер массива
                int[][] result = new int[size][];
                for (int i = 0; i < size; i++)
                {
                    result[i] = new int[] { intervals[i, 0], intervals[i, 1] };
                }
                return result; 
            }
        }
    }
}
