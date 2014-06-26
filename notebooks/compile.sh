IFS=$'\n'
FILES=$(ls *.ipynb)
for f in $FILES; do
	ipython nbconvert $f
	ipython nbconvert --to slides $f
done
