import random
from utils import *


class Labelme2Coco:

    def __init__(self, img_folder_path, line_pixel=3, big_data_path=None):
        self.line_pixel = line_pixel
        self.images = []
        self.annotations = []
        self.categories = []
        self.img_id = 0
        self.anno_id = 0
        self.big_data_path = big_data_path
        # 存放所有cls name
        self.classname_to_id = []
        self._init_imgs_and_annos(img_folder_path)
        self._init_categories()
        print('coco instance initialized.')

    def save_coco_json(self, save_path):
        instance = {'info': 'spytensor created',
                    'license': 'license',
                    'images': self.images,
                    'annotations': self.annotations,
                    'categories': self.categories}
        instance_to_json(instance, save_path)
        # 打印出所有cls, 供配置文件使用
        print('coco file saved.', self.classname_to_id)

    def _init_categories(self):
        if self.big_data_path is not None:
            instance = json_to_instance(self.big_data_path)
            self.classname_to_id = [category['name'] for category in instance['categories']]
        self.classname_to_id = sorted(self.classname_to_id)
        for i, cls in enumerate(self.classname_to_id):
            category = {}
            category['id'] = i
            category['name'] = cls
            self.categories.append(category)
        for annotation in self.annotations:
            annotation['category_id'] = self.classname_to_id.index(annotation['category_id'])

    def _init_imgs_and_annos(self, img_folder_path):
        files = os.listdir(img_folder_path)
        for file in files:
            img_file_path = os.path.join(img_folder_path, file)
            # 过滤文件夹和非图片文件
            if not os.path.isfile(img_file_path) or file[file.rindex('.')+1:] not in IMG_TYPES: continue
            # 对应的json文件
            json_file_path = os.path.join(img_folder_path, file[:file.rindex('.')]+'.json')
            try:
                instance = json_to_instance(json_file_path)
            except Exception as e:
                # 若为无目标图片
                print('\033[1;33m%s has no json file in %s. So as an empty instance.\033[0m' % (file, img_folder_path))
                instance = create_empty_json_instance(img_file_path)
            instance_clean(instance)
            image = {'id': self.img_id, 'file_name': file}
            image['width'], image['height'] = instance['imageWidth'], instance['imageHeight']
            for obj in instance['shapes']:
                self._init_annos(obj)
            self.images.append(image)
            self.img_id += 1

    def _init_annos(self, obj):
        label = obj['label']
        if label not in self.classname_to_id: self.classname_to_id.append(label)
        annotation = {'id': self.anno_id, 'iscrowd': 0, 'area': 1.0,
                      'image_id': self.img_id,
                      'category_id': label}
        try:
            annotation['segmentation'] = points_to_coco_segmentation(obj, self.line_pixel)
        except Exception as e:
            annotation['segmentation'] = [[None]]
            print('\033[1;31m%s fails in converting into segmentation...\033[0m' % (obj))
        annotation['bbox'] = points_to_xywh(obj)
        self.annotations.append(annotation)
        self.anno_id += 1

def seperate_train_test_val(json_file_path, test_ratio, val_ratio):
    instance = json_to_instance(json_file_path)
    imgs = instance['images']
    num = len(imgs)
    test = random.sample(imgs, int(num*test_ratio))
    val = random.sample(test, int(num*val_ratio))

if __name__ == '__main__':
    # 通常labelme json文件和图片在一个folder目录
    obj = Labelme2Coco(img_folder_path='/home/qiangde/Data/important/huawei_cemian_val_1219/crop',
                       # 小样本label不全，可以引入大样本json文件补充
                       big_data_path=None)
    # coco json导出路径
    obj.save_coco_json(save_path='train.json')

























