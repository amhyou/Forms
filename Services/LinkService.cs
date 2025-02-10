using Sqids;

public class LinkService
{
    private SqidsEncoder<int> sqids = new SqidsEncoder<int>(new()
    {
        MinLength = 10
    });

    public string EncryptId(int id)
    {
        return sqids.Encode(id);
    }

    public int DecryptId(string encryptedId)
    {
        return sqids.Decode(encryptedId).Single();
    }
}