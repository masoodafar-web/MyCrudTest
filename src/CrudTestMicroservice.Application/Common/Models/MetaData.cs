namespace CrudTestMicroservice.Application.Common.Models;
public class MetaData
{
    //صفحه جاری
    public long CurrentPage { get; set; }
    //تعداد کل صفحات
    public long TotalPage { get; set; }
    //تعداد در هر صفحه
    public long PageSize { get; set; }
    //تعداد کل آیتم‌ها
    public long TotalCount { get; set; }
    //قبلی دارد؟
    public bool HasPrevious { get; set; }
    //بعدی دارد؟
    public bool HasNext { get; set; }
}
