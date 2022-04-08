public static class PageHelper
{
    /// <summary>
    ///     获取最后一页的页码
    /// </summary>
    /// <param name="count"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static int GetLastPageIndex(this int count, int size)
    {
        return count % size == 0 ? count / size : count / size + 1;
    }

    /// <summary>
    ///     获取总页数
    /// </summary>
    /// <param name="count"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static int GetPageSum(this int count, int size)
    {
        return count % size == 0 ? count / size : count / size + 1;
    }

    /// <summary>
    ///     生成页码
    /// </summary>
    /// <param name="count">总条数</param>
    /// <param name="size">单页大小</param>
    /// <param name="currIndex">当前页标</param>
    /// <param name="num">取前后多少页</param>
    /// <returns></returns>
    public static int[] GenPageIndexList(this int count, int size, int currIndex, int num)
    {
        if (currIndex < 1) throw new ArgumentException("currIndex不能小于1");

        var list = new List<int>();
        var pageCount = count % size == 0 ? count / size : count / size + 1;
        if (pageCount == 0) return Array.Empty<int>();

        for (var i = currIndex - num; i <= currIndex; i++)
            if (i > 0)
                list.Add(i);

        if (list.Contains(currIndex) == false) list.Add(currIndex);

        for (var i = currIndex + 1; i <= currIndex + num; i++)
            if (i <= pageCount)
                list.Add(i);

        return list.ToArray();
    }
}