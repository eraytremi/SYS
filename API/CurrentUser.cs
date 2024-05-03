namespace API
{
    public static class CurrentUser
    {
        public static int? Get(HttpContext httpContext)
        {
            try
            {
                var ccl = httpContext.User.Claims.Where(k => k.Type.Contains("nameidentifier")).FirstOrDefault();
                if (ccl != null)
                {
                    if (int.TryParse(ccl.Value, out int result))
                    {
                        return result;
                    }
                    else
                    {
                        return null; // Dönüşüm başarısız olduysa null dönebilirsiniz.
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
