cd Stages
for file in *.csv
do
    name="${file%.*}"
    scc ../SCCTypes.csv "$file" ../Assets/Stages/"$name".stg
done