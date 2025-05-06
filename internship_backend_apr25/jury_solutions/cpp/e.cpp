#include <bits/stdc++.h>

using namespace std;
typedef long long ll;

mt19937 rnd(chrono::steady_clock::now().time_since_epoch().count());

#pragma region Trie

struct trie_vertex
{
	map<char, trie_vertex*> next;
	ll count;
	trie_vertex ()
	{
		count = 0;
	}
};

trie_vertex* trie_add_string (const string& s, trie_vertex* root, int toAdd = 1)
{
	trie_vertex* _node = root;
	_node->count += toAdd;
	for (auto& i : s)
	{
		char c = i - 'a';
		if (_node->next[c] == nullptr)
			_node->next[c] = new trie_vertex();
		_node = _node->next[c];
		_node->count += toAdd;
	}

	return _node;
}

int trie_count_prefix (const string& pref, trie_vertex* root)
{
	trie_vertex* _node = root;
	for (auto& i : pref)
	{
		char c = i - 'a';
		if (_node->next[c] == nullptr)
			return 0;
		_node = _node->next[c];
	}

	return _node->count;
}

trie_vertex* trie_expand_prefix (const string& prefix, trie_vertex* root)
{
	if (root->count == 0)
		return root;
	trie_vertex* newRoot = new trie_vertex();
	trie_vertex* leaf = trie_add_string(prefix, newRoot, root->count);

	for (auto& i : root->next)
		leaf->next[i.first] = i.second;

	return newRoot;
}

#pragma endregion Trie

#pragma region Treap

struct node 
{
	ll height, index;
	trie_vertex* server = nullptr;
	node *l = nullptr, *r = nullptr;
};

int node_count (node* _node) 
{
	return _node ? _node->index : 0;
}

void node_update_count (node* it) 
{
	if (it)
		it->index = node_count(it->l) + node_count(it->r) + 1;
}

void node_merge (node* & t, node* l, node* r) 
{
	if (!l || !r)
		t = l ? l : r;
	else if (l->height > r->height)
		node_merge(l->r, l->r, r), t = l;
	else
		node_merge(r->l, l, r->l), t = r;
	node_update_count(t);
}

void node_split (node* t, node* & l, node* & r, int key, int add = 0) 
{
	if (!t)
	{
		l = r = nullptr;
		return;
	}
	int cur_key = add + node_count(t->l);
	if (key <= cur_key)
		node_split(t->l, l, t->l, key, add), r = t;
	else
		node_split(t->r, t->r, r, key, add + 1 + node_count(t->l)), l = t;
	node_update_count(t);
}

void node_output (node* t) 
{
	if (!t)
		return;
	node_output(t->l);
	cout << t->server << ' ';
	node_output(t->r);
}

node* node_create ()
{
	node *result = new node();
	result->server = new trie_vertex();
	result->index = 1;
	result->height = rnd();
	return result;
}

node* node_insert (node *array, int position, node* insert)
{
	node *l, *r;
	node_split(array, l, r, position);

	node *res = nullptr;

	node_merge(res, l, insert);
	node_merge(array, res, r);

	return array;
}

node* node_add (node *array, int position)
{
	node* insert = node_create();
	node *l, *r;
	node_split(array, l, r, position);

	node *res = nullptr;

	node_merge(res, l, insert);
	node_merge(array, res, r);

	return array;
}

node* node_get (node *array, int pos)
{
	int count = node_count(array->l) + 1;
	if (count == pos)
		return array;
	if (count < pos)
		return node_get(array->r, pos - count);
	return node_get(array->l, pos);
}

#pragma endregion Treap

#pragma region TwoQueue

struct TwoQueue
{
	int first = -1, last = -1;

	void add(int index)
	{
		first = last;
		last = index;
	}
};

#pragma endregion TwoQueue

int main ()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);
	cout.tie(0);
	cin.sync_with_stdio(0);
	cout.sync_with_stdio(0);
	srand(time(0));
	cout.precision(20);

	int clustersCount, limit, q;
	cin >> clustersCount >> limit >> q;
	vector<node*> clusters(clustersCount, nullptr);

	vector<int> clusters_limits(clustersCount, 0);
	vector<TwoQueue*> clusters_queries(clustersCount, nullptr);

	for (int i = 0; i < clustersCount; i++)
	{
		clusters_queries[i] = new TwoQueue();

		int serversCount;
		cin >> serversCount;
		if (serversCount != 0)
			clusters[i] = node_create();
		for (int j = 1; j < serversCount; j++) 
			clusters[i] = node_add(clusters[i], j);
	}

	while (q--)
	{
		char type;
		cin >> type;
		int cluster, server;
		string s;
		cin >> cluster >> server >> s;
		cluster--;

		clusters_limits[cluster]++;
		clusters_queries[cluster]->add(server);
		node* currentServer = node_get(clusters[cluster], server);

		switch (type)
		{
			case '+':
				trie_add_string(s, currentServer->server);
				break;
			case 'p':
				currentServer->server = trie_expand_prefix(s, currentServer->server);
				break;
			case 'c':
				cout << trie_count_prefix(s, currentServer->server) << endl;
				break;
		}

		if (clusters_limits[cluster] == limit)
		{
			clusters_limits[cluster] = 0;
			int nextCluster = (cluster + 1) % clustersCount;
			int from = min(clusters_queries[cluster]->first, clusters_queries[cluster]->last) - 1;
			int to = max(clusters_queries[cluster]->first, clusters_queries[cluster]->last);
			node *left, *right, *middle, *newCluster;

			node_split(clusters[cluster], left, right, to);
			node_split(left, left, middle, from);
			node_merge(newCluster, left, right);
			clusters[cluster] = newCluster;

			if (clusters[nextCluster] == nullptr)
				clusters[nextCluster] = middle;
			else
				clusters[nextCluster] = node_insert(clusters[nextCluster], clusters[nextCluster]->index / 2, middle);

		}
	}

	return 0;
}
