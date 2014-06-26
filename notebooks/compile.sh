IFS=$'\n'
FILES=$(ls *.ipynb)
for f in $FILES; do
	ipython nbconvert $f
done
