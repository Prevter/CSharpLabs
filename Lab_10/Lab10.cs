namespace Labs.Lab10
{
	public static class Lab10
	{
		public static int[] CreateRandomArray(int size, int min, int max)
		{
			int[] array = new int[size];
			Random random = new();

			for (int i = 0; i < array.Length; i++)
				array[i] = random.Next(min, max + 1);

			return array;
		}

		public static int[] Sift(int[] array, Func<int, bool> predicate)
		{
			List<int> result = new();

			foreach (int item in array)
				if (predicate(item))
					result.Add(item);

			return result.ToArray();
		}

		public static int FindMax(int[] array)
		{
			int max = array[0];

			for (int i = 1; i < array.Length; i++)
				if (array[i] > max)
					max = array[i];

			return max;
		}

		public static int FindMin(int[] array)
		{
			int min = array[0];

			for (int i = 1; i < array.Length; i++)
				if (array[i] < min)
					min = array[i];

			return min;
		}

		public static string ArrayCaption<T>(IEnumerable<T> array, string label = "")
		{
			return $"{(string.IsNullOrWhiteSpace(label) ? "" : label + ": ")}[{string.Join(", ", array)}]";
		}
	}
}