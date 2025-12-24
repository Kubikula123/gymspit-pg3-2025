static bool AddValue(string value, string[] data, int count)
{
    if (count >= data.Length)
    {
        Console.WriteLine("I'm afraid I can't do that.");
        return false;
    }

    data[count] = value;
    return true;
}

static bool RemoveValue(string[] data, int index, int count)
{
    if (index < 0 || index >= count)
    {
        Console.WriteLine("I'm afraid I can't do that.");
        return false;
    }

    for (int i = index; i < count - 1; i += 1)
    {
        data[i] = data[i + 1];
    }
    data[count - 1] = "";
    return true;
}


static void AddUser(string username, string[] users, ref int userCount)
{
    int index = Array.IndexOf(users, username);
    if (index >= 0)
    {
        Console.WriteLine("User already exists.");
        return;
    }

    if (AddValue(username, users, userCount))
    {
        userCount += 1;
    }
}

static void RemoveUser(string username, string[] users, ref int userCount)
{
    int index = Array.IndexOf(users, username);
    if (index < 0)
    {
        Console.WriteLine("User does not exist.");
        return;
    }

    if (index >= 0 && RemoveValue(users, index, userCount))
    {
        userCount -= 1;
    }
}


static void AddPost(string post, string author, string[] users, string[] posts, string[] postAuthors, ref int postCount)
{
    int index = Array.IndexOf(posts, post);
    int index2 = Array.IndexOf(users, author);
    if (index >= 0)
    {
        Console.WriteLine("Post already exists.");
        return;
    }
    if (index2 < 0)
    {
        Console.WriteLine("Author doesn't exist");
        return;
    }
    if (AddValue(post, posts, postCount))
    {
        postAuthors[postCount] = author;
        postCount += 1;
    }
}

static string[] GetUserPosts(string user, string[] posts, string[] postAuthors, int postCount)
{
    string[] result = new string[postCount];
    int count = 0;

    for (int i = 0; i < postCount; i++)
    {
        if (postAuthors[i] == user)
        {
            result[count++] = posts[i];
        }
    }

    Array.Resize(ref result, count);
    return result;
}


static void AddFollow(string follower, string followee, string[] followers, string[] followees, ref int followCount)
{
    if (follower == followee)
    {
        Console.WriteLine("User cannot follow itself");
        return;
    }
    for (int i = 0; i < followCount; i++)
    {
        if (followers[i] == follower && followees[i] == followee)
        {
            Console.WriteLine("Already following this user.");
            return;
        }
    }
    followers[followCount] = follower;
    followees[followCount] = followee;
    followCount++;
    return;
}

static void RemoveFollow(string follower, string followee, string[] followers, string[] followees, ref int followCount)
{
    if (follower == followee)
    {
        Console.WriteLine("User cannot unfollow itself");
        return;
    }
    for (int i = 0; i < followCount; i++)
    {
        if (followers[i] == follower && followees[i] == followee)
        {
            for (int j = i; j < followCount - 1; j++)
            {
                followers[j] = followers[j + 1];
                followees[j] = followees[j + 1];
            }
            followCount--;
            return;
        }
    }
    Console.WriteLine("You aren't following this user");
}

static string[] GetUserFollows(string user, string[] followers, string[] followees, int followCount)
{
    string[] result = new string[followCount];
    int count = 0;

    for (int i = 0; i < followCount; i++)
    {
        if (followers[i] == user)
        {
            result[count++] = followees[i];
        }
    }

    Array.Resize(ref result, count);
    return result;
}

static string[] GetUserFollowers(string user, string[] followers, string[] followees, int followCount)
{
    string[] result = new string[followCount];
    int count = 0;

    for (int i = 0; i < followCount; i++)
    {
        if (followees[i] == user)
        {
            result[count++] = followers[i];
        }
    }

    Array.Resize(ref result, count);
    return result;
}


// Bonus
static string[] GetUserTimeline(string user, string[] posts, string[] postAuthors, int postCount, string[] followers, string[] followees, int followCount)
{
    // TODO
    return new string[] { };
}


int MAX_USERS = 100;
int MAX_POSTS = MAX_USERS * 100;
int MAX_FOLLOWS = MAX_USERS * (MAX_USERS + 1) / 2;

string[] users = new string[MAX_USERS];
int userCount = 0;

string[] posts = new string[MAX_POSTS];
string[] postAuthors = new string[MAX_POSTS];
int postCount = 0;

string[] followers = new string[MAX_FOLLOWS];
string[] followees = new string[MAX_FOLLOWS];
int followCount = 0;

AddUser("wormik", users, ref userCount);
AddUser("kubikula123", users, ref userCount);
AddUser("08mishi", users, ref userCount);
AddPost("hello", "wormik", users, posts, postAuthors, ref postCount);
foreach (var x in GetUserPosts("wormik", posts, postAuthors, postCount))
    Console.WriteLine(x);
AddFollow("08mishi", "wormik", followers, followees, ref followCount);
AddFollow("wormik", "kubikula123", followers, followees, ref followCount);
RemoveFollow("08mishi", "wormik", followers, followees, ref followCount);
foreach (var y in GetUserFollows("wormik", followers, followees, followCount))
    Console.WriteLine(y);

foreach (var z in GetUserFollowers("kubikula123", followers, followees, followCount))
    Console.WriteLine(z);
GetUserFollowers("wormik", followers, followees, followCount);
